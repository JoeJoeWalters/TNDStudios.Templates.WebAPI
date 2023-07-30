using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using WebAPI.Contracts;
using WebAPI.Workers;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Contacts
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class HealthController : ControllerBase, IHealthController
    {
        private readonly ILogger<IHealthController> _logger;
        private readonly IWorkerState _workerState;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="worker"></param>
        public HealthController(ILogger<IHealthController> logger, IWorkerState workerState)
        {
            _logger = logger;
            _workerState = workerState;
        }

        /// <summary>
        /// Ping for load balancer in Azure to ensure the site is running
        /// </summary>
        /// <returns>Always Ok response to indicate it's up</returns>
        [HttpGet]
        [Route("Healthcheck")]
        public IActionResult Healthcheck()
        {
            return Ok();
        }

        /// <summary>
        /// Endpoint to report on health of services
        /// </summary>
        /// <returns>Summary of health of services</returns>
        [HttpGet]
        [Route("State")]
        public IActionResult State()
        {
            HealthCheckResult result = _workerState.IsRunning ? HealthCheckResult.Healthy("Worker Running.") : HealthCheckResult.Unhealthy("Worker Not Running.");

            return Ok(result);
        }
    }
}