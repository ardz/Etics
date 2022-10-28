using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Etics.IntegrationTests.Http
{
    public class HttpTestClient
    {
        private static HttpClient? _client;

        private readonly bool _httpLogging;

        private readonly string _baseAddress;

        private HttpClient? Client
        {
            get
            {
                if (_client != null)
                {
                    return _client;
                }

                _client = _httpLogging
                    ? new HttpClient(new HttpLoggingHandler())
                    {
                        BaseAddress = new Uri(_baseAddress)
                    }
                    : new HttpClient();

                _client.BaseAddress = new Uri(_baseAddress);

                return _client;
            }
        }

        public HttpTestClient(string baseAddress, bool logging = false)
        {
            _baseAddress = baseAddress;
            _httpLogging = logging;
        }

        public HttpTestClient Authorise(string token)
        {
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            return this;
        }

        public HttpTestClient AddRequestHeader(string name, string value)
        {
            Client.DefaultRequestHeaders.Add(name, value);

            return this;
        }

        public HttpTestClient ClearRequestHeaders()
        {
            Client.DefaultRequestHeaders.Clear();

            return this;
        }

        public async Task<HttpResponseMessage> Post(string route, string content, string mediaType = "application/json")
        {
            var request = new HttpRequestMessage
            {
                Content = new StringContent(content, Encoding.UTF8, "application/json"),
                Method = HttpMethod.Post,
                RequestUri = new Uri(_baseAddress + "/" + route)
            };

            var response = await Client.SendAsync(request);

            return response;
        }
    }
}