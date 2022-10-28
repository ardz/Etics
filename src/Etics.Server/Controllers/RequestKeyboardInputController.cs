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
    public Task<ActionResult> PostKeyboardInput(EliteShipInputCommand eliteShipInputCommand)
    {

        return null;
    }
}