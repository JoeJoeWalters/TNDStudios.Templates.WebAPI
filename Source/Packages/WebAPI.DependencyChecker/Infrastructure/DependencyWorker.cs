using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Http;
using WebAPI.DependencyChecker;
using WebAPI.DependencyChecker.Infrastructure;

namespace WebAPI.Workers
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyWorker : BackgroundService
    {
        private readonly ILogger<DependencyWorker> _logger;
        private readonly DependencyWorkerState _state;
        private readonly DependencyCheckerConfig _config;

        private Dictionary<string, bool> _previousStates;
        private HttpClient _httpClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="state"></param>
        public DependencyWorker(ILogger<DependencyWorker> logger, DependencyWorkerState state, DependencyCheckerConfig config)
        {
            _logger = logger;
            _state = state;
            _config = config;
            _previousStates = new Dictionary<string, bool>();
        }

        private bool? PreviousState(IDependencyCheck check)
        {
            // Because TryGetValue only provides "false" not Bool? = null
            if (_previousStates.ContainsKey(check.Id))
                return _previousStates[check.Id];
            else
                return null;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _httpClient = new HttpClient();
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _httpClient.Dispose();
            return base.StopAsync(cancellationToken);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // To ensure good recovery then don't let exceptions other than cancellation exceptions bubble out of here
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    // Perform each check
                    foreach (IDependencyCheck check in _config.Checks)
                    {
                        IDependencyCheckResult? checkResult = null;

                        // Depending on the check type we can execute the appropriate functionality
                        switch (check)
                        {
                            case HttpCheck:

                                checkResult = ExecuteHttpCheck((HttpCheck)check);

                                break;
                        }

                        // Do we have a hook to call and has the state changed?
                        if (checkResult != null &&
                            checkResult.SuccessState != checkResult.PreviousState)
                        {
                            // Swap the previous states before the hook for debugging (as the debugging can stop the state setting for the next loop)
                            _previousStates[check.Id] = checkResult.SuccessState;

                            // Call the hook
                            if (check.OnChange != null)
                                check.OnChange(checkResult);
                        }
                    }

                    // Record the last run time and then wait for a given length of time to run again
                    _state.LastRan = DateTime.UtcNow;
                    _logger.LogInformation("Running checks at '{time}'", _state.LastRan);
                }
                catch (OperationCanceledException ex)
                {
                    return; // Exit the infinite loop
                }
                catch (Exception ex)
                {
                    // Handle any errors and ensure the while loop isn't broken so we can recover
                }
                finally
                {
                    await Task.Delay(_config.Frequency, stoppingToken);
                }
            }
        }

        protected IDependencyCheckResult ExecuteHttpCheck(HttpCheck check)
        {
            HttpCheckResult result = new HttpCheckResult() { Origin = check };

            var httpResult = _httpClient.GetAsync(new Uri(check.Path)).Result;

            result.SuccessState = check.ExpectedResults.Contains(httpResult.StatusCode);
            result.StatusCode = httpResult.StatusCode;
            result.PreviousState = PreviousState(check) ?? (result.SuccessState ? result.SuccessState : true); // If the first ever check is a failure, report the previous state as working so it kicks off the hooks otherwise they will never fire

            return result;
        }
    }
}