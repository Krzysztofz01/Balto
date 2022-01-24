using Balto.Application.Authentication;
using Balto.Cli.Client;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Balto.Cli.Remote
{
    public class BaltoHttpClient : HttpClient
    {
        private const string _refreshTokenCookieName = "balto_refresh_token";

        private readonly ClientConfiguration _clientConfiguration;

        public BaltoHttpClient(ClientConfiguration clientConfiguration) : base()
        {
            _clientConfiguration = clientConfiguration;

            BaseAddress = new Uri(_clientConfiguration.ServerAddress);

            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task Authenticate()
        {
            if (_clientConfiguration.RefreshToken is not null)
            {
                DefaultRequestHeaders.Add("Cookie", $"{_refreshTokenCookieName}={_clientConfiguration.RefreshToken}");

                var refreshResponse = await PostAsync(Endpoints.Refresh, JsonContent.Create(new { }));

                if (refreshResponse.IsSuccessStatusCode)
                {
                    SetRefreshToken(refreshResponse);

                    var serializedRefreshContent = await refreshResponse.Content.ReadAsStringAsync();
                    var deserializedRefreshContent = JsonSerializer.Deserialize<Responses.V1.Refresh>(serializedRefreshContent);

                    DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", deserializedRefreshContent.JsonWebToken);
                    return;
                }
            }

            var serializedAuthBody = JsonContent.Create(new Requests.V1.Login
            {
                Email = _clientConfiguration.Email,
                Password = _clientConfiguration.Password
            });

            var authResponse = await PostAsync(Endpoints.Authenticate, serializedAuthBody);

            if (!authResponse.IsSuccessStatusCode)
                throw new InvalidOperationException("Authentication failed.");

            SetRefreshToken(authResponse);

            var serializedContent = await authResponse.Content.ReadAsStringAsync();
            var deserializedContent = JsonSerializer.Deserialize<Responses.V1.Login>(serializedContent);

            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", deserializedContent.JsonWebToken);
        }

        private void SetRefreshToken(HttpResponseMessage httpResponseMessage)
        {
            httpResponseMessage.Headers.TryGetValues("Set-Cookie", out var cookiesHeader);
            if (cookiesHeader is not null)
            {
                var cookies = cookiesHeader
                    .Single()
                    .Split(';', StringSplitOptions.TrimEntries);

                foreach (var cookie in cookies)
                {
                    var valuePair = cookie.Split('=');

                    if (valuePair.Length != 2) continue;
                    if (valuePair.FirstOrDefault() != _refreshTokenCookieName) continue;

                    var refreshToken = valuePair.Last();

                    _clientConfiguration.RefreshToken = refreshToken;
                    break;
                }
            }
        }
    }
}
