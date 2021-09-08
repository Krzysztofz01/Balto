using System;

namespace Balto.Application.Aggregates.Note
{
    public static class Commands
    {
        public static class V1
        {
            public class NoteAdd
            {
                public string Title { get; set; }
                public string Content { get; set; }
            }

            public class NoteDelete
            {
                public Guid Id { get; set; }
            }

            public class NoteUpdate
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
                public string Content { get; set; }
            }

            public class NoteChangePublication
            {
                public Guid Id { get; set; }
            }

            public class NoteAddContributor
            {
                public Guid Id { get; set; }
                public Guid UserId { get; set; }
            }

            public class NoteDeleteContributor
            {
                public Guid Id { get; set; }
                public Guid UserId { get; set; }
            }

            public class NoteLeave
            {
                public Guid Id { get; set; }
            }
        }
    }
}
