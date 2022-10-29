using Etics.Server.Abstractions;
using Etics.Server.Service.Models;
using WindowsInput;
using WindowsInput.Native;

namespace Etics.Server.Service;

public class KeyboardInputService : IKeyboardInputService
{
    private readonly InputSimulator _simulator;
    private readonly InputTranslator _inputTranslator;

    public KeyboardInputService()
    {
        _simulator = new InputSimulator();
        _inputTranslator = new InputTranslator();
    }

    public void KeyboardInputHandler(DateTime timestamp, string[] keyboardCommands, string? summary)
    {
        var modifierKeyboardInputs = new List<KeyboardInput>();
        var keyboardInputs = new List<KeyboardInput>();

        var modifierCount = 0;
        var keyCount = 0;

        foreach (var command in keyboardCommands)
        {
            var key = _inputTranslator.GetVirtualKeyCode(command);

            if (key.IsModifier)
            {
                modifierKeyboardInputs.Add(key);
            }
            else
            {
                keyboardInputs.Add(key);
            }
        }

        var modifiers = modifierKeyboardInputs.Select(x => x.VirtualKeyCode);
        var modifierKeyCodes = modifiers as VirtualKeyCode[] ?? modifiers.ToArray();
        modifierCount = modifierKeyCodes.ToList().Count;
        
        var keys = keyboardInputs.Select(x => x.VirtualKeyCode);
        var keyCodes = keys as VirtualKeyCode[] ?? keys.ToArray();
        var virtualKeyCodes = keys as VirtualKeyCode[] ?? keyCodes.ToArray();
        keyCount = virtualKeyCodes.ToList().Count;

        switch (modifierCount)
        {
            case > 0:
                SendModifiedKeystrokes(modifierKeyCodes, virtualKeyCodes);

                return;
            case 0 when keyCount > 0:
                SendKeySequence(keyCodes);
                break;
        }
    }

    private void SendModifiedKeystrokes(IEnumerable<VirtualKeyCode> modifierKeyCodes,
        IEnumerable<VirtualKeyCode> keyCodes)
    {
        _simulator.Keyboard.ModifiedKeyStroke(modifierKeyCodes, keyCodes);
    }

    private void SendKeySequence(IEnumerable<VirtualKeyCode> keyCodes)
    {
        foreach (var key in keyCodes)
        {
            _simulator.Keyboard.KeyPress(key);
        }
    }
}