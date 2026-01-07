using Microsoft.AspNetCore.Mvc;
using PortfolioAPI.Models;

namespace PortfolioAPI.Controllers
{
    [ApiController]
    [Route("api/contact")]
    public class ContactController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendMessage([FromBody] ContactMessage contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Email))
            {
                return BadRequest("Email is required");
            }

            // Later: Save to database / send email
            return Ok(new
            {
                success = true,
                message = "Message received successfully 🚀"
            });
        }
    }
}
