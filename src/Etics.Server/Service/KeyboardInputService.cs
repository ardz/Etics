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

    public void KeyboardInputHandler(DateTime timestamp, string[] keyboardCommands, string? summary)
    {
        if (keyboardCommands.Length > 1)
        {
            // SendModifiedKeystroke();
            
            return;
        }
    }

    private void SendModifiedKeystrokes(IEnumerable<VirtualKeyCode> modifierKeyCodes,
        IEnumerable<VirtualKeyCode> keyCodes)
    {
        _simulator.Keyboard.ModifiedKeyStroke(modifierKeyCodes, keyCodes);
    }

    private void SendKey(VirtualKeyCode keyCode)
    {
        _simulator.Keyboard.KeyPress(keyCode);
    }
}