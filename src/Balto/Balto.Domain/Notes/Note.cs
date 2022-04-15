using Balto.Domain.Core.Events;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;
using Balto.Domain.Notes.NoteContributors;
using Balto.Domain.Notes.NoteTags;
using System;
using System.Collections.Generic;

namespace Balto.Domain.Notes
{
    public class Note : AggregateRoot
    {
        public NoteTitle Title { get; private set; }
        public NoteContent Content { get; private set; }

        private readonly List<NoteContributor> _contributors;
        public IReadOnlyCollection<NoteContributor> Contributors => _contributors.SkipDeleted().AsReadOnly();

        private readonly List<NoteTag> _tags;
        public IReadOnlyCollection<NoteTag> Tags => _tags.SkipDeleted().AsReadOnly();

        protected override void Handle(IEventBase @event)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        public Note()
        {
            _contributors = new List<NoteContributor>();
            _tags = new List<NoteTag>();
        }
    }
}
