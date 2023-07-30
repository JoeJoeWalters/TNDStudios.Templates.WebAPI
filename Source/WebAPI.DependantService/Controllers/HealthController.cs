using Microsoft.AspNetCore.Mvc;

namespace WebAPI.DependantService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HealthController : ControllerBase
    {
        private readonly ILogger<HealthController> _logger;

        public HealthController(ILogger<HealthController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Ping for load balancer in Azure to ensure the site is running
        /// </summary>
        /// <returns>Always Ok response to indicate it's up</returns>
        [HttpGet]
        [Route("Healthcheck")]
        public IActionResult Healthcheck() => Ok();
    }
}