using Balto.Application.Authentication;
using Balto.Cli.Client;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;

namespace Balto.Cli.Remote
{
#pragma warning disable CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning disable CS0618 // Type or member is obsolete
    public class BaltoHttpClient : HttpClient
    {
        private readonly ClientConfiguration _clientConfiguration;

        private static readonly JsonSerializerOptions _jsonSerializerOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        [Obsolete("Use the AuthenticatedPostAsync method with automatic authentication and retry instead.")]
        public new async Task<HttpResponseMessage> PostAsync(string? requestUri, HttpContent content) => await base.PostAsync(requestUri, content);

        [Obsolete("Use the AuthenticatedPutAsync method with automatic authentication and retry instead.")]
        public new async Task<HttpResponseMessage> PutAsync(string? requestUri, HttpContent content) => await base.PutAsync(requestUri, content);

        [Obsolete("Use the AuthenticatedGetAsync method with automatic authentication and retry instead.")]
        public new async Task<HttpResponseMessage> GetAsync(string? requestUri) => await base.GetAsync(requestUri);

        [Obsolete("Use the AuthenticatedDeleteAsync method with automatic authentication and retry instead.")]
        public new async Task<HttpResponseMessage> DeleteAsync(string? requestUri) => await base.DeleteAsync(requestUri);

        private BaltoHttpClient(ClientConfiguration clientConfiguration) : base() =>
            _clientConfiguration = clientConfiguration;

        public async Task<TResponseType> AuthenticatedPostAsync<TResponseType>(string requestUri, HttpContent content) where TResponseType : class
        {
            var postResponse = await PostAsync(requestUri, content);

            if (postResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Authenticate();

                postResponse = await PostAsync(requestUri, content);
            }

            var serializedResponse = await postResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponseType>(serializedResponse, _jsonSerializerOptions);
        }

        public async Task<TResponseType> AuthenticatedGetAsync<TResponseType>(string requestUri) where TResponseType : class
        {
            var getResponse = await GetAsync(requestUri);

            if (getResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Authenticate();

                getResponse = await GetAsync(requestUri);
            }

            var serializedResponse = await getResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponseType>(serializedResponse, _jsonSerializerOptions);
        }

        public async Task<TResponseType> AuthenticatedPutAsync<TResponseType>(string requestUri, HttpContent content) where TResponseType : class
        {
            var putResponse = await PutAsync(requestUri, content);

            if (putResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Authenticate();

                putResponse = await PutAsync(requestUri, content);
            }

            var serializedResponse = await putResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponseType>(serializedResponse, _jsonSerializerOptions);
        }

        public async Task<TResponseType> AuthenticatedDeleteAsync<TResponseType>(string requestUri) where TResponseType : class
        {
            var deleteResponse = await DeleteAsync(requestUri);

            if (deleteResponse.StatusCode == HttpStatusCode.Unauthorized)
            {
                await Authenticate();

                deleteResponse = await DeleteAsync(requestUri);
            }

            var serializedResponse = await deleteResponse.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<TResponseType>(serializedResponse, _jsonSerializerOptions);
        }

        private async Task Authenticate()
        {
            if (_clientConfiguration.RefreshToken is not null)
            {
                var serializedRefreshBody = JsonContent.Create(new Requests.V1.Refresh
                {
                    RefreshToken = _clientConfiguration.RefreshToken
                });

                var refreshResponse = await PostAsync(Endpoints.Refresh, serializedRefreshBody);

                if (refreshResponse.IsSuccessStatusCode)
                {
                    var serializedRefreshContent = await refreshResponse.Content.ReadAsStringAsync();
                    var deserializedRefreshContent = JsonSerializer.Deserialize<Responses.V1.Refresh>(serializedRefreshContent, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    });

                    _clientConfiguration.RefreshToken = deserializedRefreshContent.RefreshToken;

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

            var serializedAuthContent = await authResponse.Content.ReadAsStringAsync();
            var deserializedAuthContent = JsonSerializer.Deserialize<Responses.V1.Login>(serializedAuthContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            _clientConfiguration.RefreshToken = deserializedAuthContent.RefreshToken;

            DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", deserializedAuthContent.JsonWebToken);
        }

        public static async Task<BaltoHttpClient> CreateInstance(ClientConfiguration clientConfiguration)
        {
            var client = new BaltoHttpClient(clientConfiguration)
            {
                BaseAddress = new Uri(clientConfiguration.ServerAddress),
            };

            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            await client.Authenticate();

            return client;
        }
    }
#pragma warning restore CS8632 // The annotation for nullable reference types should only be used in code within a '#nullable' annotations context.
#pragma warning restore CS0618 // Type or member is obsolete
}
