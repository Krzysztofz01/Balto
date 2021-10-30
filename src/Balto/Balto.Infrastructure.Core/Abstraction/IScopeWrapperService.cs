using System;

namespace Balto.Infrastructure.Core.Abstraction
{
    public interface IScopeWrapperService
    {
        string GetIpAddress();
        Guid GetUserId();
        string GetUserRole();
        void SetRefreshTokenCookie(string refreshTokenCookieValue);
        string GetRefreshTokenCookie();
    }
}
