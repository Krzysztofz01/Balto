using System;

namespace Balto.Domain.Core.Events
{
    public interface IEvent : IEventBase
    {
        Guid Id { get; set; }
    }
}
