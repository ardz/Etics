using Etics.Server.Abstractions;
using WindowsInput;
using WindowsInput.Native;

namespace Etics.Server.Service;

public class KeyboardInputService : IKeyboardInputService
{
    private readonly InputSimulator _simulator;

    public KeyboardInputService()
    {
        _simulator = new InputSimulator();
    }

    public void Foo(DateTime timestamp, string[] keyboardCommands, string summary)
    {
        if (keyboardCommands.Length > 1)
        {
            
            
            SendModifiedKeystroke();
            
            return;
        }
        
        SendKeyPress();
    }

    private void SendKeyPress()
    {
        
    }

    private void SendModifiedKeystroke()
    {
        _simulator.Keyboard.ModifiedKeyStroke(VirtualKeyCode.MENU, VirtualKeyCode.SPACE);
    }

    public void SendKeystrokes(ClientInputCommand clientInputCommand)
    {
        throw new NotImplementedException();
    }
}