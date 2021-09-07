using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Note
{
    public class NoteContributorId : Value<NoteContributorId>
    {
        public Guid Value { get; private set; }

        protected NoteContributorId() { }
        public NoteContributorId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "User id value can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(NoteContributorId self) => self.Value;
        public static NoteContributorId NoUser => new NoteContributorId();
    }
}
