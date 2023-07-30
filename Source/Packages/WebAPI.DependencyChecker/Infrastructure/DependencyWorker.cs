using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WebAPI.DependencyChecker;

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
                    List<IDependencyCheck> failed = new List<IDependencyCheck>();

                    // Do each check
                    foreach(IDependencyCheck check in _config.Checks)
                    {
                        failed.Add(check);
                    }

                    if (_config.OnFailed != null && failed.Any()) { _config.OnFailed(failed); }

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