using WindowsInput;

namespace Etics.Server.Service;

public class KeyboardInputService
{
    private readonly InputSimulator _simulator;

    public KeyboardInputService()
    {
        this._simulator = new InputSimulator();
    }

    public void SendKeystrokes()
    {
    }
}