using Microsoft.AspNetCore.Mvc;

namespace Shorter.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ShorterController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<ShorterController> _logger;

        public ShorterController(ILogger<ShorterController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public Task<string> Generate()
        {
            return Task.FromResult("teste");
        }
    }
}