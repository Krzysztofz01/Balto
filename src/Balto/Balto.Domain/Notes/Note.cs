using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;
using Balto.Domain.Notes.NoteContributors;
using Balto.Domain.Notes.NoteSnapshots;
using Balto.Domain.Notes.NoteTags;
using System;
using System.Collections.Generic;

namespace Balto.Domain.Notes
{
    public class Note : AggregateRoot
    {
        public NoteTitle Title { get; private set; }
        public NoteContent Content { get; private set; }
        public NoteOwnerId OwnerId { get; private set; }

        private readonly List<NoteContributor> _contributors;
        public IReadOnlyCollection<NoteContributor> Contributors => _contributors.SkipDeleted().AsReadOnly();

        private readonly List<NoteTag> _tags;
        public IReadOnlyCollection<NoteTag> Tags => _tags.SkipDeleted().AsReadOnly();

        public readonly List<NoteSnapshot> _snapshots;
        public IReadOnlyCollection<NoteSnapshot> Snapshoots => _snapshots.SkipDeleted().AsReadOnly();

        protected override void Handle(IEventBase @event)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            bool isNull = Title == null || Content == null ||
                OwnerId == null || Tags == null || Contributors == null || Snapshoots == null;

            if (isNull)
                throw new BusinessLogicException("The note aggregate properties can not be null.");
        }

        public Note()
        {
            _contributors = new List<NoteContributor>();
            _tags = new List<NoteTag>();
            _snapshots = new List<NoteSnapshot>();
        }
    }
}
