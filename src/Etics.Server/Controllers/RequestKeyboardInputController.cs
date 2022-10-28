using Etics.Server.Service;
using Microsoft.AspNetCore.Mvc;

namespace Etics.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestKeyboardInputController : ControllerBase
{
    private readonly ILogger<RequestKeyboardInputController> _logger;
    private readonly KeyboardInputService _keyboardInputService;
    private readonly GameLogTailingService _gameLogTailingService;

    public RequestKeyboardInputController(ILogger<RequestKeyboardInputController> logger,
        KeyboardInputService keyboardInputService, GameLogTailingService gameLogTailingService)
    {
        _logger = logger;
        _keyboardInputService = keyboardInputService;
        _gameLogTailingService = gameLogTailingService;
    }

    [HttpPost(Name = "PostKeyboardInput")]
    public async Task<IActionResult> PostKeyboardInput([FromBody] EliteShipInputCommand eliteShipInputCommand)
    {
        _keyboardInputService.SendKeystrokes();

        return this.Ok();
    }
}