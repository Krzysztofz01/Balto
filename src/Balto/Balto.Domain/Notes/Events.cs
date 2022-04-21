using Balto.Domain.Core.Events;
using Balto.Domain.Notes.NoteContributors;
using System;

namespace Balto.Domain.Notes
{
    public static class Events
    {
        public static class V1
        {
            // Note aggregate root related events
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

            // Contributor entity related events
            public class NoteContributorAdded : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public Guid UserId { get; set; }
            }

            public class NoteContributorDeleted : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public Guid UserId { get; set; }
            }

            public class NoteContributorUpdated : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public Guid UserId { get; set; }
                public ContributorAccessRole Role { get; set; }
            }

            public class NoteContributorLeft : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
            }

            // Tag entity related events
            public class NoteTagAssigned : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public Guid TagId { get; set; }
            }

            public class NoteTagUnassigned : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public Guid TagId { get; set; }
            }

            // Snapshoot related events

        }
    }
}
