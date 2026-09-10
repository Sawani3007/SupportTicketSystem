using Microsoft.AspNetCore.Mvc;

namespace SupportTicketSystem.Api.Controllers
{
    [ApiController]
    [Route("api/settings")]
    public class SettingsController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SettingsController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        public IActionResult GetSettings()
        {
            int defaultPageSize =
                _configuration.GetValue<int>("DefaultPageSize");
            return Ok(new
            {
                DefaultPageSize = defaultPageSize
            });
        }
    }
}