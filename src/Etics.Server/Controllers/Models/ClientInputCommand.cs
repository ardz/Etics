namespace Etics.Server.Controllers.Models;

public class ClientInputCommand
{
    public DateTime Date { get; set; }

    public string[] Keys { get; set; }

    public string? Summary { get; set; }
}