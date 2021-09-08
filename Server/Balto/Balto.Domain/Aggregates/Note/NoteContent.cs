using Balto.Domain.Common;

namespace Balto.Domain.Aggregates.Note
{
    public class NoteContent : Value<NoteContent>
    {
        public string Value { get; private set; }

        protected NoteContent() { }
        protected NoteContent(string value)
        {
            Value = (value is null) ? string.Empty : value;
        }

        public static implicit operator string(NoteContent note) => note.Value;

        public static NoteContent FromString(string value) => new NoteContent(value);
        public static NoteContent NoContent => new NoteContent(string.Empty);
    }
}
