using Balto.Domain.Core.Events;
using System;

namespace Balto.Domain.Notes
{
    public static class Events
    {
        public static class V1
        {
            public class NoteCreated : IAuthorizableEvent
            {
                public Guid CurrentUserId { get; set; }
                public string Title { get; set; }
            }

            public class NoteUpdated : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public string Title { get; set; }
                public string Content { get; set; }
            }

            public class NoteDeleted : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
            }

            // Contributore related events

            // Tags related events

            // Snapshoot related events

        }
    }
}
