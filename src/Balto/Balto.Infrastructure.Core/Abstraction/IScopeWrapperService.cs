using Balto.Domain.Identities;
using System;

namespace Balto.Infrastructure.Core.Abstraction
{
    public interface IScopeWrapperService
    {
        string GetIpAddress();
        Guid GetUserId();
        UserRole GetUserRole();
        void SetRefreshTokenCookie(string refreshTokenCookieValue);
        string GetRefreshTokenCookie();
    }
}
