using Etics.Server.Abstractions;
using Etics.Server.Service.Models;
using WindowsInput;
using WindowsInput.Native;

namespace Etics.Server.Service;

public class EnglishKeyboardInputService : IKeyboardInputService
{
    private readonly InputSimulator _simulator;
    private readonly StringToVirtualKeyCodeTranslator _inputTranslator;

    public EnglishKeyboardInputService()
    {
        _simulator = new InputSimulator();
        _inputTranslator = new StringToVirtualKeyCodeTranslator();
    }

    public void KeyboardInputHandler(DateTime timestamp, string[]? keyboardCommands, string? summary)
    {
        var modifierKeyboardInputs = new List<KeyboardInput>();
        var keyboardInputs = new List<KeyboardInput>();

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
        var modifierKeyCodes = modifiers.ToList();
        var modifierCount = modifierKeyCodes.ToList().Count;
        
        var keys = keyboardInputs.Select(x => x.VirtualKeyCode);
        var virtualKeyCodes = keys.ToList();
        var keyCount = virtualKeyCodes.ToList().Count;

        if (modifierCount > 0 && keyCount > 0)
        {
            SendModifiedKeystrokes(modifierKeyCodes, virtualKeyCodes);
            
            return;
        }

        switch (modifierCount)
        {
            case > 0 when keyCount == 0:
                SendKeySequence(modifierKeyCodes);
                break;
            case 0 when keyCount > 0:
                SendKeySequence(virtualKeyCodes);
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