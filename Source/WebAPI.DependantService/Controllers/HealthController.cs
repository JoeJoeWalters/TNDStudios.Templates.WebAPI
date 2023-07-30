using Microsoft.AspNetCore.Mvc;
using System.Net;

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
        public IActionResult Healthcheck()
            => (DateTime.UtcNow.Second > 30) ? Ok() : new StatusCodeResult((int)HttpStatusCode.InternalServerError); // Switch to a success / failure state every 30 seconds to test the dependency checking in the web api
    }
}