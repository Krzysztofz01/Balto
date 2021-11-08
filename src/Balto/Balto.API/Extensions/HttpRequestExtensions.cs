using Microsoft.AspNetCore.Http;
using System;

namespace Balto.API.Extensions
{
    public static class HttpRequestExtensions
    {
        public static Uri GetCurrentUri(this HttpRequest request)
        {
            var uriBuilder = new UriBuilder
            {
                Scheme = request.Scheme,
                Host = request.Host.Host,
                Port = request.Host.Port.GetValueOrDefault(80),
                Path = request.Path.ToString(),
                Query = request.QueryString.ToString()
            };

            return uriBuilder.Uri;
        }
    }
}
