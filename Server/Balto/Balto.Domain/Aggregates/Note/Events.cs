using System;

namespace Balto.Domain.Aggregates.Note
{
    public static class Events
    {
        public class NoteCreated
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public class NoteUpdated
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public class NoteDeleted
        {
            public Guid Id { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class NotePublicationChanged
        {
            public Guid Id { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class NoteContributorAdded
        {
            public Guid Id { get; set; }
            public Guid ContributorId { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class NoteContributorDeleted
        {
            public Guid Id { get; set; }
            public Guid ContributorId { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class NoteLeave
        {
            public Guid Id { get; set; }
            public Guid CurrentUserId { get; set; }
        }
    }
}
