using Etics.Server.Exceptions;
using Etics.Server.Service;

namespace Etics.InputServiceTests;

public class Tests
{
    private static StringToVirtualKeyCodeTranslator Translator => new();
    
    [Fact]
    public void InvalidKeyStringsSpecified_ExpectTranslationError()
    {
        var keys = new[] { "CTRL", "SHIFT" };

        foreach (var key in keys)
        {
            void Act() => Translator.GetVirtualKeyCode(key);

            Assert.Throws<InputKeyTranslationException>((Action)Act);
        }
    }
}