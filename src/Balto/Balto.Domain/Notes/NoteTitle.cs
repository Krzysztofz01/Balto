using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Notes
{
    public class NoteTitle : ValueObject<NoteTitle>
    {
        private const int _maxLength = 100;

        public string Value { get; private set; }

        private NoteTitle() { }
        private NoteTitle(string value)
        {
            if (value.IsEmpty() || !value.IsLengthLess(_maxLength))
                throw new ValueObjectValidationException("Invalid note title length.");

            Value = value;
        }

        public static implicit operator string(NoteTitle title) => title.Value;

        public static NoteTitle FromString(string title) => new NoteTitle(title);
    }
}
