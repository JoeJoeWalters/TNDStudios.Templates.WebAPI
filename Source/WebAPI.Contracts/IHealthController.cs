using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Contracts
{
    public interface IHealthController
    {
        public IActionResult Healthcheck();
        public IActionResult State();
    }
}
