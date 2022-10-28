using Microsoft.AspNetCore.Mvc;

namespace Etics.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestKeyboardInputController : ControllerBase
{
    private readonly ILogger<RequestKeyboardInputController> _logger;

    public RequestKeyboardInputController(ILogger<RequestKeyboardInputController> logger)
    {
        _logger = logger;
    }

    //[HttpGet(Name = "GetWeatherForecast")]
    public void Post()
    {
        // return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        //     {
        //         Date = DateTime.Now.AddDays(index),
        //         TemperatureC = Random.Shared.Next(-20, 55),
        //         Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        //     })
        //     .ToArray();
    }
}