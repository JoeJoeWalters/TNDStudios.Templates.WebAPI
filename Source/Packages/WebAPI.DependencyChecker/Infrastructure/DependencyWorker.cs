using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Net;
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
                    foreach(IDependencyCheck check in _config.Checks)
                    {
                        IDependencyCheckResult? checkResult = null;

                        // Depending on the check type we can execute the appropriate functionality
                        switch (check)
                        {
                            case HttpCheck:

                                checkResult = await ExecuteHttpCheck((HttpCheck)check);

                                break;
                        }

                        // Do we have a hook to call and has the state changed?
                        if (checkResult != null &&
                            checkResult.SuccessState != checkResult.PreviousState)
                        {
                            // Call the hook
                            if (check.OnChange != null) 
                                check.OnChange(checkResult);

                            // Hook was called (maybe) so now set the previous state to be the current one
                            _previousStates.Add(check.Id, checkResult.SuccessState); 
                        }
                    }

                    // Record the last run time and then wait for a given length of time to run again
                    _state.LastRan = DateTime.UtcNow;
                    _logger.LogInformation("Running checks at '{time}'", _state.LastRan);
                    await Task.Delay(_config.Frequency, stoppingToken);
                }
                catch (OperationCanceledException ex)
                {
                    return; // Exit the infinite loop
                }
                catch (Exception ex)
                {
                    // Handle any errors and ensure the while loop isn't broken so we can recover
                }
            }
        }

        protected async Task<IDependencyCheckResult> ExecuteHttpCheck(HttpCheck check)
        {
            HttpCheckResult result = new HttpCheckResult() { Origin = check };
            
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(check.Path);
            var httpResult = await httpClient.GetAsync(httpClient.BaseAddress);

            result.SuccessState = check.ExpectedResults.Contains(httpResult.StatusCode);
            result.PreviousState = PreviousState(check) ?? result.SuccessState;

            return result;
        }
    }
}