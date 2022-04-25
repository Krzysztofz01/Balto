using Balto.Domain.Notes.NoteContributors;
using System;
using System.Collections.Generic;

namespace Balto.Application.Notes
{
    public static class Dto
    {
        public class NoteSimple
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
            public string Title { get; set; }
            public IEnumerable<Guid> TagIds { get; set; }
        }

        public class NoteDetails
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public IEnumerable<ContributorDetails> Contributors { get; set; }
            public IEnumerable<SnapshotDetails> Snapshots { get; set; }
            public IEnumerable<Guid> TagIds { get; set; }
        }

        public class ContributorDetails
        {
            public Guid Id { get; set; }
            public ContributorAccessRole AccessRole { get; set; }
        }

        public class SnapshotDetails
        {
            public Guid Id { get; set; }
            public string Content { get; set; }
            public DateTime CreationDate { get; set; }
        }
    }
}
