using Balto.Application.Settings;
using Balto.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Balto.Application.Monitoring
{
    public class MonitoringService : IMonitoringService
    {
        private readonly BaltoDbContext _dbContext;
        private readonly MonitoringSettings _monitoringSettings;
        private readonly IHttpClientFactory _httpClientFactory;

        private const string _serverAddress = "http://www.localhost:7004/api/v1";
        private const string _headerName = "X-Balto-Monitoring";

        public MonitoringService(
            BaltoDbContext dbContext,
            IOptions<MonitoringSettings> monitoringSettings,
            IHttpClientFactory httpClientFactory)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));

            _monitoringSettings = monitoringSettings.Value ??
                throw new ArgumentNullException(nameof(monitoringSettings));

            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        public async Task Ping(bool startup = false)
        {
            var client = _httpClientFactory.CreateClient();

            var address = (startup) ? "/ping?startup=true" : "/ping";

            var request = new HttpRequestMessage(HttpMethod.Post, $"{ _serverAddress }{ address }");

            request.Headers.Add(_headerName, DateTime.Now.ToString());

            await client.SendAsync(request);
        }

        public async Task ReportException(Exception e, string message = null)
        {
            if (_monitoringSettings.AdvancedMonitoring)
            {
                var client = _httpClientFactory.CreateClient();

                var report = new { Exception = $"{ e.Message } { e.StackTrace }", Message = (message is null) ? string.Empty : message };

                var content = new StringContent(JsonSerializer.Serialize(report), Encoding.UTF8, "application/json");

                content.Headers.Add(_headerName, DateTime.Now.ToString());

                await client.PostAsync($"{ _serverAddress }/exception", content);
            }
        }

        public async Task ReportInstanceStatus()
        {
            if (_monitoringSettings.AdvancedMonitoring)
            {
                var client = _httpClientFactory.CreateClient();

                var userAmount = await _dbContext.Users
                    .CountAsync();

                var report = new { userAmount };

                var content = new StringContent(JsonSerializer.Serialize(report), Encoding.UTF8, "application/json");

                content.Headers.Add(_headerName, DateTime.Now.ToString());

                await client.PostAsync($"{ _serverAddress }/instance", content);
            }
        }
    }
}
