using Balto.Application.Settings;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Balto.Application.Telemetry
{
    public class TelemetryService : ITelemetryService
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly TelemetrySettings _telemetrySettings;

        private const string _telemetryHeader = "X-Balto-Telemetry";

        public TelemetryService(
            IHttpClientFactory httpClientFactory,
            IOptions<TelemetrySettings> telemetrySettings)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));

            _telemetrySettings = telemetrySettings.Value ??
                throw new ArgumentNullException(nameof(telemetrySettings));
        }

        public async Task LogException(Exception exception, string message = null)
        {
            var client = _httpClientFactory.CreateClient();

            var report = new { Exception = exception, Message = (message is null) ? string.Empty : message };

            var content = new StringContent(JsonSerializer.Serialize(report), Encoding.UTF8, "application/json");

            content.Headers.Add(_telemetryHeader, DateTime.Now.ToString());

            await client.PostAsync($"{ PrepareRequestPath() }/exception", content);
        }

        public async Task Ping()
        {
            var client = _httpClientFactory.CreateClient();

            var request = new HttpRequestMessage(HttpMethod.Post, $"{ PrepareRequestPath() }/ping");

            request.Headers.Add(_telemetryHeader, DateTime.Now.ToString());

            await client.SendAsync(request);
        }

        private string PrepareRequestPath()
        {
            return $"{_telemetrySettings.TelemetryServerUrl}/api/v{_telemetrySettings.TelemetryServerVersion}";
        }
    }
}
