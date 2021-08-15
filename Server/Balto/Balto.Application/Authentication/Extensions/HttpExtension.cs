using Microsoft.AspNetCore.Http;
using System;

namespace Balto.Application.Authentication.Extensions
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
    }
}
