using Balto.Application.Settings;
using Balto.Infrastructure.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Linq.Expressions;
using System.Security.Claims;

namespace Balto.Application.Authorization
{
    public class AccessQueryFilterHandler : IAccessQueryFilterHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserRoleNameSettings _userRoleNameSettings;

        public AccessQueryFilterHandler(
            IHttpContextAccessor httpContextAccessor,
            IOptions<UserRoleNameSettings> userRoleNameSettings)
        {
            _httpContextAccessor = httpContextAccessor ??
                throw new ArgumentNullException(nameof(httpContextAccessor));

            _userRoleNameSettings = userRoleNameSettings.Value ??
                throw new ArgumentNullException(nameof(userRoleNameSettings));
        }

        public bool IsAllowed(Guid contentOwnerGuid)
        {
            if (GetRoleValue() == _userRoleNameSettings.Leader) return true;

            return IsAllowedIgnoreLeaders(contentOwnerGuid);
        }

        public bool IsAllowedIgnoreLeaders(Guid contentOwnerGuid)
        {
            return GetRequestUserGuidValue() == contentOwnerGuid;
        }

        public Expression<Func<T, bool>> Rule<T>(Expression<Func<T, bool>> ruleBuilder)
        {
            return ruleBuilder;
        }

        private string GetRoleValue()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
        }

        private Guid GetRequestUserGuidValue()
        {
            return Guid.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
