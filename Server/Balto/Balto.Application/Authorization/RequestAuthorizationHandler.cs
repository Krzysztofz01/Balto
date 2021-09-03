using Balto.Domain.Extensions;
using Balto.Infrastructure.Abstraction;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Balto.Application.Authorization
{
    public class RequestAuthorizationHandler : IRequestAuthorizationHandler
    {
        private const string _refreshTokenCookieName = "balto_refresh_cookie";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestAuthorizationHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ??
                throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public string GetIpAddress()
        {
            string ip = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!ip.IsEmpty()) ip = ip.Split('.').First();

            if (ip.IsEmpty()) ip = Convert.ToString(_httpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress);

            if (ip.IsEmpty()) ip = _httpContextAccessor.HttpContext.Request.Headers["REMOTE_ADDR"].FirstOrDefault();

            return ip;
        }

        public string GetRefreshTokenCookie()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[_refreshTokenCookieName];
        }

        public Guid GetUserGuid()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public string GetUserRole()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
        }

        public void SetRefreshTokenCookie(string refreshTokenValue)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(_refreshTokenCookieName, refreshTokenValue, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(7)
            });
        }
    }
}
