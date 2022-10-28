using Etics.Server.Abstractions;
using WindowsInput;

namespace Etics.Server.Service;

public class KeyboardInputService : IKeyboardInputService
{
    private readonly InputSimulator _simulator;

    public KeyboardInputService()
    {
        _simulator = new InputSimulator();
    }

    public void SendKeystrokes(ClientInputCommand clientInputCommand)
    {
        throw new NotImplementedException();
    }
}