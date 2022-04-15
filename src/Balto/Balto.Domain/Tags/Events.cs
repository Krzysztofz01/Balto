using Balto.Domain.Core.Events;
using System;

namespace Balto.Domain.Tags
{
    public static class Events
    {
        public static class V1
        {
            public class TagCreated : IEventBase
            {
                public string Title { get; set; }
                public string Color { get; set; }
            }

            public class TagDeleted : IEvent
            {
                public Guid Id { get; set; }
            }
        }
    }
}
