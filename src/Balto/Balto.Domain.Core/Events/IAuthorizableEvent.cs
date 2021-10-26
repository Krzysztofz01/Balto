using System;

namespace Balto.Domain.Core.Events
{
    public interface IAuthorizableEvent
    {
        Guid CurrentUserId { get; set; }
    }
}
