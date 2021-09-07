using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Note
{
    public class NoteId : Value<NoteId>
    {
        public Guid Value { get; internal set; }

        protected NoteId() { }
        public NoteId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Note id can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(NoteId self) => self.Value;
        public static implicit operator NoteId(string value) => new NoteId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
