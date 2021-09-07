using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Note
{
    public class NoteOwnerId : Value<NoteOwnerId>
    {
        public Guid Value { get; private set; }

        protected NoteOwnerId() { }
        public NoteOwnerId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "User id value can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(NoteOwnerId self) => self.Value;
        public static NoteOwnerId NoUser => new NoteOwnerId();
    }
}
