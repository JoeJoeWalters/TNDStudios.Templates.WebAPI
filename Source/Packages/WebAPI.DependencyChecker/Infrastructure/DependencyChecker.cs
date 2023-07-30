namespace WebAPI.Workers
{
    /// <summary>
    /// 
    /// </summary>
    public class DependencyChecker : BackgroundService
    {
        private readonly ILogger<DependencyChecker> _logger;
        private readonly IDependencyCheckerState _state;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="state"></param>
        public DependencyChecker(ILogger<DependencyChecker> logger, IDependencyCheckerState state)
        {
            _logger = logger;
            _state = state;
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
                    //https://localhost:7049/health/healthcheck

                    _state.LastRan = DateTime.UtcNow;
                    _logger.LogInformation("Running operation at '{time}'", _state.LastRan);
                    await Task.Delay(1000, stoppingToken);
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