using System;
using System.Collections.Generic;
using System.Text;

namespace Balto.Infrastructure.Authentication
{
    public interface IRequestContext
    {
        bool UserHasAccess(Guid contentOwnerGuid);
        string UserGetIpAddress();
        Guid UserGetGuid();

    }
}
