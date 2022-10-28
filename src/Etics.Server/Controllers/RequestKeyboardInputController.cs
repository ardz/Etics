using Etics.Server.Abstractions;
using Etics.Server.Controllers.Models;
using Microsoft.AspNetCore.Mvc;

namespace Etics.Server.Controllers;

[ApiController]
[Route("[controller]")]
public class RequestKeyboardInputController : ControllerBase
{
    private readonly ILogger<RequestKeyboardInputController> _logger;
    private readonly IKeyboardInputService _keyboardInputService;

    public RequestKeyboardInputController(ILogger<RequestKeyboardInputController> logger,
        IKeyboardInputService keyboardInputService)
    {
        _logger = logger;
        _keyboardInputService = keyboardInputService;
    }

    [HttpPost(Name = "PostKeyboardInput")]
    public async Task<IActionResult> PostKeyboardInput([FromBody] ClientInputCommand clientInputCommand)
    {
        _keyboardInputService.KeyboardInputHandler(clientInputCommand.Date, clientInputCommand.Keys,
            clientInputCommand.Summary);

        return Ok();
    }
}