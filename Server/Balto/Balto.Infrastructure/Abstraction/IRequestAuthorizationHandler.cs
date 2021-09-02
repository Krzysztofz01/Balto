﻿using System;

namespace Balto.Infrastructure.Abstraction
{
    public interface IRequestAuthorizationHandler
    {
        string GetIpAddress();
        Guid GetUserGuid();
        string GetUserRole();
    }
}