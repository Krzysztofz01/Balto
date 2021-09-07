using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Note
{
    public class NoteTitle : Value<NoteTitle>
    {
        public string Value { get; private set; }

        protected NoteTitle() { }
        protected NoteTitle(string value)
        {
            Validate(value);

            Value = value;
        }

        private static void Validate(string value)
        {
            if (value.Length > 100) throw new ArgumentOutOfRangeException(nameof(value), "Note tittle can not be longer than 100 characters.");
        }

        public static implicit operator string(NoteTitle title) => title.Value;

        public static NoteTitle FromString(string value) => new NoteTitle(value);
    }
}
