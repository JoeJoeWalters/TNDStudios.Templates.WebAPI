namespace WebAPI
{
    /// <summary>
    /// 
    /// </summary>
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        private Boolean _isRunning = false;
        /// <summary>
        /// 
        /// </summary>
        public Boolean IsRunning 
        { 
            get { return _isRunning; } 
            set { _isRunning = value; } 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
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
                    _isRunning = true;
                    _logger.LogInformation("Running operation at '{time}'", DateTimeOffset.Now);
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