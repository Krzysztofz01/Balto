using System;

namespace Balto.Domain.Core.Events
{
    public interface IAuthorizableEvent : IEventBase
    {
        Guid CurrentUserId { get; set; }
    }
}
