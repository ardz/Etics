using Etics.Server.Exceptions;
using Etics.Server.Service.Models;
using WindowsInput.Native;

namespace Etics.Server.Service;

public class InputTranslator
{
    private readonly IDictionary<string, VirtualKeyCode> _modifierKeyMapper = new Dictionary<string, VirtualKeyCode>
    {
        // modifiers
        { "LEFT_SHIFT", VirtualKeyCode.LSHIFT },
        { "RIGHT_SHIFT", VirtualKeyCode.RSHIFT },
        { "LEFT_ALT", VirtualKeyCode.LMENU },
        { "RIGHT_ALT", VirtualKeyCode.RMENU },
        { "LEFT_WINDOWS", VirtualKeyCode.LWIN },
        { "RIGHT_WINDOWS", VirtualKeyCode.RWIN },
        { "APPS", VirtualKeyCode.APPS },
        { "RIGHT_CONTROL", VirtualKeyCode.RCONTROL },
        { "LEFT_CONTROL", VirtualKeyCode.LCONTROL },
    };

    private readonly IDictionary<string, VirtualKeyCode> _keyMapper = new Dictionary<string, VirtualKeyCode>
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

        // print screen, scroll lock, pause
        { "PRINT_SCREEN", VirtualKeyCode.SNAPSHOT },
        { "SCROLL_LOCK", VirtualKeyCode.SCROLL },
        { "PAUSE", VirtualKeyCode.PAUSE },

        // insert [to] page down
        { "INSERT", VirtualKeyCode.INSERT },
        { "HOME", VirtualKeyCode.HOME },
        { "PAGE_UP", VirtualKeyCode.PRIOR },
        { "DEL", VirtualKeyCode.DELETE },
        { "END", VirtualKeyCode.END },
        { "PAGE_DOWN", VirtualKeyCode.NEXT },

        // backspace
        { "BACKSPACE", VirtualKeyCode.BACK },

        // enter
        { "ENTER", VirtualKeyCode.RETURN },

        // tab
        { "TAB", VirtualKeyCode.TAB },

        // caps
        { "CAPS", VirtualKeyCode.CAPITAL },
        
        // space
        { "SPACE", VirtualKeyCode.SPACE },

        // non alpha numeric chars
        { "CONSOLE", VirtualKeyCode.OEM_8 },
        { "MINUS", VirtualKeyCode.OEM_MINUS },
        { "PLUS", VirtualKeyCode.OEM_PLUS },
        { "SQUARE_BRACKET_LEFT", VirtualKeyCode.OEM_4 },
        { "SQUARE_BRACKET_RIGHT", VirtualKeyCode.OEM_6 },
        { "SEMI_COLON", VirtualKeyCode.OEM_1 }, // ( ; )
        { "SINGLE_QUOTE", VirtualKeyCode.OEM_3 }, //  ( ' )
        { "HASHTAG", VirtualKeyCode.OEM_7 }, //  ( # )
        { "COMMA", VirtualKeyCode.OEM_COMMA }, //  ( , )
        { "PERIOD", VirtualKeyCode.OEM_PERIOD }, //  ( . )
        { "FORWARD_SLASH", VirtualKeyCode.OEM_2 }, // ( / )
        { "BACKSLASH", VirtualKeyCode.OEM_5 }, //  ( \ )

        // numbers
        { "1", VirtualKeyCode.VK_1 },
        { "2", VirtualKeyCode.VK_2 },
        { "3", VirtualKeyCode.VK_3 },
        { "4", VirtualKeyCode.VK_4 },
        { "5", VirtualKeyCode.VK_5 },
        { "6", VirtualKeyCode.VK_6 },
        { "7", VirtualKeyCode.VK_7 },
        { "8", VirtualKeyCode.VK_8 },
        { "9", VirtualKeyCode.VK_9 },
        { "0", VirtualKeyCode.VK_0 },
        { "NUM1", VirtualKeyCode.NUMPAD1 },
        { "NUM2", VirtualKeyCode.NUMPAD2 },
        { "NUM3", VirtualKeyCode.NUMPAD3 },
        { "NUM4", VirtualKeyCode.NUMPAD4 },
        { "NUM5", VirtualKeyCode.NUMPAD5 },
        { "NUM6", VirtualKeyCode.NUMPAD6 },
        { "NUM7", VirtualKeyCode.NUMPAD7 },
        { "NUM8", VirtualKeyCode.NUMPAD8 },
        { "NUM9", VirtualKeyCode.NUMPAD9 },
        { "NUM0", VirtualKeyCode.NUMPAD0 },

        // arithmetic
        { "MULTIPLY", VirtualKeyCode.MULTIPLY },
        { "ADD", VirtualKeyCode.ADD },
        { "SUBTRACT", VirtualKeyCode.SUBTRACT },
        { "DIVIDE", VirtualKeyCode.DIVIDE },
        { "DECIMAL", VirtualKeyCode.DECIMAL },

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

        // arrows
        { "LEFT", VirtualKeyCode.LEFT },
        { "UP", VirtualKeyCode.UP },
        { "DOWN", VirtualKeyCode.DOWN },
        { "RIGHT", VirtualKeyCode.RIGHT },
    };

    public KeyboardInput GetVirtualKeyCode(string input)
    {
        var keyboardInput = new KeyboardInput();

        var keyCode = _keyMapper.ContainsKey(input)
            ? _keyMapper[input]
            : throw new InputKeyTranslationException($"Couldn't map {input} to a valid virtual key code!");
        
        keyboardInput.IsModifier = _modifierKeyMapper.ContainsKey(input);
        keyboardInput.VirtualKeyCode = keyCode;

        return keyboardInput;
    }
}