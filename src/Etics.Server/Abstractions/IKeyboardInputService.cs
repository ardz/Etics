namespace Etics.Server.Abstractions;

public interface IKeyboardInputService
{
    void SendKeystrokes(ClientInputCommand clientInputCommand);
}