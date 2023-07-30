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
        }

        private bool PreviousState(IDependencyCheck check) => false;

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
                    DependencyCheckResults results = new DependencyCheckResults();

                    // Do each check
                    foreach(IDependencyCheck check in _config.Checks)
                    {
                        switch (check)
                        {
                            case HttpCheck:

                                results.Checks.Add(new HttpCheckResult() {Origin = check, StatusCode = HttpStatusCode.OK, SuccessState = true, PreviousState = PreviousState(check) });

                                break;
                        }
                    }

                    if (_config.OnCheck != null && results.Checks.Any()) { _config.OnCheck(results); }

                    _state.LastRan = DateTime.UtcNow;
                    _logger.LogInformation("Running operation at '{time}'", _state.LastRan);
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
    }
}