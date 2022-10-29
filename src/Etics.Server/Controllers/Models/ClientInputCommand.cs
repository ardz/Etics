namespace Etics.Server.Controllers.Models;

public struct ClientInputCommand
{
    public DateTime Date { get; init; }

    public string[]? Keys { get; init; }

    public string? Summary { get; init; }
}