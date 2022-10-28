namespace Etics.Server.Exceptions;

public class InputKeyTranslationException : Exception
{
    public InputKeyTranslationException()
    {
    }

    public InputKeyTranslationException(string message)
        : base(message)
    {
    }

    public InputKeyTranslationException(string message, Exception inner)
        : base(message, inner)
    {
    }
}