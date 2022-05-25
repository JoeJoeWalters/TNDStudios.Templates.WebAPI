using Domain;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [ApiVersion("1.0")]
    public class ContactController : ControllerBase
    {
        private readonly ILogger<ContactController> _logger;

        public ContactController(ILogger<ContactController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Query the available stored contacts
        /// </summary>
        /// <returns>An array of contacts</returns>
        [HttpGet]
        [Route("/Query")]
        public IEnumerable<ContactVM> QueryContacts()
        {
            return new List<ContactVM>() { };
        }
    }
}