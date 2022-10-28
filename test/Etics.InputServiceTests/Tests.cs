using Etics.Server;
using Etics.Server.Controllers.Models;

namespace Etics.InputServiceTests;

public class Tests
{
    [Fact]
    public void Test1()
    {
        var command = new ClientInputCommand
        {
            Date = DateTime.Now,
            Keys = new[] { "CTRL", "SHIFT", "F" },
            Summary = "Engage frameshift drive"
        };
    }
}