using Balto.Application.Abstraction;
using Balto.Domain.Notes;
using Balto.Domain.Notes.NoteContributors;
using System;

namespace Balto.Application.Notes
{
    public static class Commands
    {
        public static class V1
        {
            public class Create : IApplicationCommand<Note>
            {
                public string Title { get; set; }
            }

            public class Update : IApplicationCommand<Note>
            {
                public Guid Id { get; set; }

                public string Title { get; set; }

                public string Content { get; set; }
            }

            public class Delete : IApplicationCommand<Note>
            {
                public Guid Id { get; set; }
            }

            public class AddContributor : IApplicationCommand<Note>
            {
                public Guid Id { get; set; }

                public Guid UserId { get; set; }
            }

            public class DeleteContributor : IApplicationCommand<Note>
            {
                public Guid Id { get; set; }

                public Guid UserId { get; set; }
            }

            public class UpdateContributor : IApplicationCommand<Note>
            {
                public Guid Id { get; set; }

                public Guid UserId { get; set; }

                public ContributorAccessRole? Role { get; set; }
            }

            public class LeaveAsContributor : IApplicationCommand<Note>
            {
                public Guid Id { get; set; }
            }

            public class TagAssign : IApplicationCommand<Note>
            {
                public Guid Id { get; set; }

                public Guid TagId { get; set; }
            }

            public class TagUnassign : IApplicationCommand<Note>
            {
                public Guid Id { get; set; }

                public Guid TagId { get; set; }
            }

            public class SnapshotCreate : IApplicationCommand<Note>
            {
                public Guid Id { get; set; }
            }

            public class SnapshotDelete : IApplicationCommand<Note>
            {
                public Guid Id { get; set; }

                public Guid SnapshotId { get; set; }
            }
        }
    }
}
