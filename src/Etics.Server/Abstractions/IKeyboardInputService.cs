namespace Etics.Server.Abstractions;

public interface IKeyboardInputService
{
    void KeyboardInputHandler(DateTime timestamp, string[]? keyboardCommands, string? summary);
}