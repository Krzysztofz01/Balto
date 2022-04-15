using Balto.Domain.Core.Model;

namespace Balto.Domain.Notes
{
    public class NoteContent : ValueObject<NoteContent>
    {
        public string Value { get; private set; }

        private NoteContent() { }
        private NoteContent(string value)
        {
            Value = value;
        }

        public static implicit operator string(NoteContent title) => title.Value;

        public static NoteContent FromString(string title) => new NoteContent(title);
    }
}
