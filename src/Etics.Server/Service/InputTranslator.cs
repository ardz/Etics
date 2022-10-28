using WindowsInput.Native;

namespace Etics.Server.Service;

public class InputTranslator
{
    public InputTranslator()
    {
    }

    private readonly IDictionary<string, VirtualKeyCode> _keyMapper = new Dictionary<string, VirtualKeyCode>()
    {
        // escape
        { "ESC", VirtualKeyCode.ESCAPE },
        
        // function keys
        { "F1", VirtualKeyCode.F1 },
        { "F2", VirtualKeyCode.F2 },
        { "F3", VirtualKeyCode.F3 },
        { "F4", VirtualKeyCode.F4 },
        { "F5", VirtualKeyCode.F5 },
        { "F6", VirtualKeyCode.F6 },
        { "F7", VirtualKeyCode.F7 },
        { "F8", VirtualKeyCode.F8 },
        { "F9", VirtualKeyCode.F9 },
        { "F10", VirtualKeyCode.F10 },
        { "F11", VirtualKeyCode.F11 },
        { "F12", VirtualKeyCode.F12 },
        
        // letters
        { "A", VirtualKeyCode.VK_A },
        { "B", VirtualKeyCode.VK_B },
        { "C", VirtualKeyCode.VK_C },
        { "D", VirtualKeyCode.VK_D },
        { "E", VirtualKeyCode.VK_E },
        { "F", VirtualKeyCode.VK_F },
        { "G", VirtualKeyCode.VK_G },
        { "H", VirtualKeyCode.VK_H },
        { "J", VirtualKeyCode.VK_J },
        { "K", VirtualKeyCode.VK_K },
        { "L", VirtualKeyCode.VK_L },
        { "M", VirtualKeyCode.VK_M },
        { "N", VirtualKeyCode.VK_N },
        { "O", VirtualKeyCode.VK_O },
        { "P", VirtualKeyCode.VK_P },
        { "Q", VirtualKeyCode.VK_Q },
        { "R", VirtualKeyCode.VK_R },
        { "S", VirtualKeyCode.VK_S },
        { "T", VirtualKeyCode.VK_T },
        { "U", VirtualKeyCode.VK_U },
        { "V", VirtualKeyCode.VK_V },
        { "W", VirtualKeyCode.VK_W },
        { "X", VirtualKeyCode.VK_X },
        { "Y", VirtualKeyCode.VK_Y },
        { "Z", VirtualKeyCode.VK_Z },
    };

    public VirtualKeyCode TranslateInput(string inputKey)
    {
        return _keyMapper.ContainsKey(inputKey) ? _keyMapper[inputKey] : throw new Exception();
    }
}