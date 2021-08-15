using Balto.Domain.Extensions;
using Balto.Infrastructure.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Linq;
using System.Security.Claims;

namespace Balto.Application.Authentication
{
    public class RequestContext : IRequestContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public RequestContext(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ??
                throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        public Guid UserGetGuid()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }

        public string UserGetIpAddress()
        {
            string ip = _httpContextAccessor.HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();

            if (!ip.IsEmpty()) ip = ip.Split('.').First();

            if (ip.IsEmpty()) ip = Convert.ToString(_httpContextAccessor.HttpContext.Request.HttpContext.Connection.RemoteIpAddress);

            if (ip.IsEmpty()) ip = _httpContextAccessor.HttpContext.Request.Headers["REMOTE_ADDR"].FirstOrDefault();

            return ip;
        }

        public bool UserHasAccess(Guid contentOwnerGuid)
        {
            if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value == "Leader") return true;

            if (_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value == contentOwnerGuid.ToString()) return true;

            return false;
        }
    }
}
