using System;
using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Aggregates.Note
{
    public static class Commands
    {
        public static class V1
        {
            public class NoteAdd
            {
                [Required]
                public string Title { get; set; }

                [Required]
                public string Content { get; set; }
            }

            public class NoteDelete
            {
                [Required]
                public Guid Id { get; set; }
            }

            public class NoteUpdate
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public string Title { get; set; }

                [Required]
                public string Content { get; set; }
            }

            public class NoteChangePublication
            {
                [Required]
                public Guid Id { get; set; }
            }

            public class NoteAddContributor
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid UserId { get; set; }
            }

            public class NoteDeleteContributor
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid UserId { get; set; }
            }

            public class NoteLeave
            {
                [Required]
                public Guid Id { get; set; }
            }
        }
    }
}
