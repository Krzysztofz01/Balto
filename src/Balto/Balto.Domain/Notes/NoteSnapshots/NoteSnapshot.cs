using Balto.Domain.Core.Events;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Notes.NoteSnapshots
{
    public class NoteSnapshot : Entity
    {
        public NoteSnapshotContent Content { get; private set; }
        public NoteSnapshotCreationDate CreationDate { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
