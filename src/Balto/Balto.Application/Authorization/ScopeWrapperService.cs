﻿using Balto.Application.Settings;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Identities;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Security.Claims;

namespace Balto.Application.Authorization
{
    public class ScopeWrapperService : IScopeWrapperService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JsonWebTokenSettings _jwtSettings;

        private const string _refreshTokenCookieName = "balto_refresh_token";

        public ScopeWrapperService(IHttpContextAccessor httpContextAccessor, IOptions<JsonWebTokenSettings> jsonWebTokenSettings)
        {
            _httpContextAccessor = httpContextAccessor ??
                throw new ArgumentNullException(nameof(httpContextAccessor));

            _jwtSettings = jsonWebTokenSettings.Value ??
                throw new ArgumentNullException(nameof(jsonWebTokenSettings)); ;
        }

        public string GetIpAddress()
        {
            string ip = _httpContextAccessor.HttpContext.Request.Headers["X-Forwared-For"].FirstOrDefault();

            if (!ip.IsEmpty()) ip = ip.Split('.').First().Trim();

            if (ip.IsEmpty()) ip = Convert.ToString(_httpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress);

            if (ip.IsEmpty()) ip = Convert.ToString(_httpContextAccessor.HttpContext.Request.HttpContext.Connection.LocalIpAddress);

            if (ip.IsEmpty()) ip = _httpContextAccessor.HttpContext.Request.Headers["REMOTE_ADDR"].FirstOrDefault();

            if (ip.IsEmpty()) ip = string.Empty;

            return ip;
        }

        public Guid GetUserId()
        {
            var claimsValue = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (claimsValue is null) SystemAuthorizationException.Unauthenticated();

            return Guid.Parse(claimsValue);
        }

        public UserRole GetUserRole()
        {
            var claimsValue = _httpContextAccessor.HttpContext.User?.FindFirst(ClaimTypes.Role)?.Value;
            if (claimsValue is null) SystemAuthorizationException.Unauthenticated();

            return (UserRole)Enum.Parse(typeof(UserRole), claimsValue);
        }

        public void SetRefreshTokenCookie(string refreshTokenCookieValue)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append(_refreshTokenCookieName, refreshTokenCookieValue, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpirationDays)
            });
        }

        public string GetRefreshTokenCookie()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies[_refreshTokenCookieName];
        }
    }
}
