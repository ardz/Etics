using Etics.IntegrationTests.Http;

namespace Etics.IntegrationTests;

public class KeyboardInputClientTests
{
    private static HttpTestClient? _client;
    
    [SetUp]
    public void Setup()
    {
        _client = new HttpTestClient("http://localhost:????");
    }

    [Test]
    public void Test1()
    {
        Assert.Pass();
    }
}