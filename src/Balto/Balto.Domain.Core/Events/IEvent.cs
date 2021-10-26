using System;

namespace Balto.Domain.Core.Events
{
    public interface IEvent
    {
        Guid Id { get; set; }
    }
}
