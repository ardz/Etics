namespace Etics.Server.Exceptions;

public class InputServiceValidationException : Exception
{
    public InputServiceValidationException() : base(String.Empty, null)
    {
    }

    private InputServiceValidationException(string memberName, string? message = null) : base(message)
    {
        this.Error = message ?? $"{memberName} must be supplied";
        this.MemberName = memberName;
    }
    
    public string Error { get; }

    public string MemberName { get; }
}