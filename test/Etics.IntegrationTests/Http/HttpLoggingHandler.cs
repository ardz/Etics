using System.Net.Http.Headers;
using Serilog;
using Serilog.Core;

namespace Etics.IntegrationTests.Http;

public class HttpLoggingHandler : DelegatingHandler
{
    private readonly Logger _logger;

    public HttpLoggingHandler(HttpMessageHandler innerHandler = null)
        : base(innerHandler ?? new HttpClientHandler())
    {

        _logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var id = Guid.NewGuid().ToString();
        var msg = $"[{id} -   Request]";

        _logger.Information("{Msg}========Start==========", msg);
        _logger.Information("{Msg} {RequestMethod} {RequestUriPathAndQuery} {RequestUriScheme}/{RequestVersion}", msg,
            request.Method, request.RequestUri?.PathAndQuery, request.RequestUri?.Scheme, request.Version);
        _logger.Information("{Msg} Host: {RequestUriScheme}://{RequestUriHost}", msg, request.RequestUri?.Scheme,
            request.RequestUri?.Host);

        foreach (var header in request.Headers)
            _logger.Information("{Msg} {HeaderKey}: {Join}", msg, header.Key, string.Join(", ", header.Value));

        if (request.Content != null)
        {
            foreach (var header in request.Content.Headers)
                _logger.Information("{Msg} {HeaderKey}: {Join}", msg, header.Key, string.Join(", ", header.Value));

            if (request.Content is StringContent ||
                IsTextBasedContentType(request.Headers) ||
                IsTextBasedContentType(request.Content.Headers))
            {
                var result = await request.Content.ReadAsStringAsync(cancellationToken);

                _logger.Information("{Msg} Content:", msg);
                _logger.Information("{Msg} {Join}...", msg, string.Join("", result.Take(255)));
            }
        }

        var start = DateTime.Now;

        var response = await base.SendAsync(request, cancellationToken).ConfigureAwait(false);

        var end = DateTime.Now;

        _logger.Information("{Msg} Duration: {Start}", msg, end - start);
        _logger.Information("{Msg}==========End==========", msg);

        msg = $"[{id} - Response]";
        _logger.Information("{Msg}=========Start=========", msg);

        _logger.Information("{Msg} {Upper}/{ResponseVersion} {ResponseStatusCode} {ResponseReasonPhrase}", msg,
            request.RequestUri?.Scheme.ToUpper(), response.Version, (int)response.StatusCode,
            response.ReasonPhrase);

        foreach (var header in response.Headers)
            _logger.Information("{Msg} {HeaderKey}: {Join}", msg, header.Key, string.Join(", ", header.Value));

        foreach (var header in response.Content.Headers)
            _logger.Information("{Msg} {HeaderKey}: {Join}", msg, header.Key, string.Join(", ", header.Value));

        if (response.Content is StringContent ||
            IsTextBasedContentType(response.Headers) ||
            IsTextBasedContentType(response.Content.Headers))
        {
            start = DateTime.Now;
            var result = await response.Content.ReadAsStringAsync(cancellationToken);
            end = DateTime.Now;

            _logger.Information("{Msg} Content:", msg);
            _logger.Information("{Msg} {Join}...", msg, string.Join("", result.Take(255)));
            _logger.Information("{Msg} Duration: {Start}", msg, end - start);
        }

        _logger.Information("{Msg}==========End==========", msg);
        return response;
    }

    private readonly string[] types =
    {
        "html", "text", "xml", "json", "txt", "x-www-form-urlencoded"
    };

    private bool IsTextBasedContentType(HttpHeaders headers)
    {
        if (!headers.TryGetValues("Content-Type", out var values))
            return false;

        var header = string.Join(" ", values).ToLowerInvariant();

        return types.Any(t => header.Contains(t));
    }
}