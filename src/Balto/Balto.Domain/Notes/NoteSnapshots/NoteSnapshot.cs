using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;
using static Balto.Domain.Notes.Events;

namespace Balto.Domain.Notes.NoteSnapshots
{
    public class NoteSnapshot : Entity
    {
        public NoteSnapshotContent Content { get; private set; }
        public NoteSnapshotCreationDate CreationDate { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            switch (@event)
            {
                case V1.NoteSnapshotDeleted e: When(e); break;

                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = Content == null || CreationDate == null;

            if (isNull)
                throw new BusinessLogicException("The note snapshot entity properties can not be null.");
        }

        private void When(V1.NoteSnapshotDeleted _)
        {
            DeletedAt = DateTime.Now;
        }

        private NoteSnapshot() { }

        public static class Factory
        {
            public static NoteSnapshot Create(V1.NoteSnapshotCreated @event)
            {
                return new NoteSnapshot
                {
                    Content = NoteSnapshotContent.FromString(@event.Content),
                    CreationDate = NoteSnapshotCreationDate.Now
                };
            }
        }
    }
}
