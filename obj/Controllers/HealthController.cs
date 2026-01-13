using Microsoft.AspNetCore.Mvc;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/health")]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Health()
        {
            return Ok(new
            {
                status = "Healthy",
                timestamp = DateTime.UtcNow
            });
        }

        [HttpGet("live")]
        public IActionResult Live()
        {
            return Ok("Application is alive");
        }

        [HttpGet("ready")]
        public IActionResult Ready()
        {
            return Ok("Application is ready");
        }
    }
}
