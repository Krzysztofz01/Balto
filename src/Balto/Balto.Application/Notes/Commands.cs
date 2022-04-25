using Balto.Application.Abstraction;
using Balto.Domain.Notes;
using Balto.Domain.Notes.NoteContributors;
using System;
using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Notes
{
    public static class Commands
    {
        public static class V1
        {
            public class Create : IApplicationCommand<Note>
            {
                [Required]
                public string Title { get; set; }
            }

            public class Update : IApplicationCommand<Note>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public string Title { get; set; }

                [Required]
                public string Content { get; set; }
            }

            public class Delete : IApplicationCommand<Note>
            {
                [Required]
                public Guid Id { get; set; }
            }

            public class AddContributor : IApplicationCommand<Note>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid UserId { get; set; }
            }

            public class DeleteContributor : IApplicationCommand<Note>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid UserId { get; set; }
            }

            public class UpdateContributor : IApplicationCommand<Note>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid UserId { get; set; }

                [Required]
                public ContributorAccessRole Role { get; set; }
            }

            public class LeaveAsContributor : IApplicationCommand<Note>
            {
                [Required]
                public Guid Id { get; set; }
            }

            public class TagAssign : IApplicationCommand<Note>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TagId { get; set; }
            }

            public class TagUnassign : IApplicationCommand<Note>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TagId { get; set; }
            }

            public class SnapshotCreate : IApplicationCommand<Note>
            {
                [Required]
                public Guid Id { get; set; }
            }

            public class SnapshotDelete : IApplicationCommand<Note>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid SnapshotId { get; set; }
            }
        }
    }
}
