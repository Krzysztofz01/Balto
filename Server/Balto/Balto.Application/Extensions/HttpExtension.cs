using Microsoft.AspNetCore.Http;
using System;

namespace Balto.Application.Extensions
{
    public static class HttpExtension
    {
        private const string _refreshTokenCookieName = "refreshToken";

        public static void SetRefreshTokenCookie(this HttpResponse response, string refreshToken)
        {
            response.Cookies.Append(_refreshTokenCookieName, refreshToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            });
        }

        public static bool IsRefreshTokenCookiePresent(this HttpRequest request)
        {
            return request.Cookies.ContainsKey(_refreshTokenCookieName);
        }

        public static Uri GetUri(this HttpRequest request)
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
