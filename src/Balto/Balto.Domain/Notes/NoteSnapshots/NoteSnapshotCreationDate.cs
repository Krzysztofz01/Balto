using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Notes.NoteSnapshots
{
    public class NoteSnapshotCreationDate : ValueObject<NoteSnapshotCreationDate>
    {
        public DateTime Value { get; private set; }

        private NoteSnapshotCreationDate() { }
        private NoteSnapshotCreationDate(DateTime value)
        {
            if (value == default)
                throw new ValueObjectValidationException("The note snapshot creation date can not have the default value.");

            Value = value;
        }

        public static implicit operator DateTime(NoteSnapshotCreationDate creationDate) => creationDate.Value;

        public static NoteSnapshotCreationDate Now => new NoteSnapshotCreationDate(DateTime.Now);
    }
}
