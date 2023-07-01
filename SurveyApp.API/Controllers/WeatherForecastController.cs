using Microsoft.AspNetCore.Mvc;
using SurveyApp.API.Models;
using SurveyApp.Application.Services.UserService;
using SurveyApp.Domain.Entities.Users;

namespace SurveyApp.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly UserService _userService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, UserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        //[HttpPost]
        //public async Task<IActionResult> CreatePlaylist([FromBody] User playlist)
        //{
        //    await _userService.CreateUserAsync(playlist);
        //    return CreatedAtAction(nameof(GetPlaylist), new { id = playlist.Id }, playlist);
        //}

        //[HttpGet]
        //public async Task<List<User>> GetPlaylist()
        //{
        //    return await _userService.GetUsersAsync();
        //}
    }
}