using Balto.Domain.Core.Model;

namespace Balto.Domain.Notes.NoteSnapshots
{
    public class NoteSnapshotContent : ValueObject<NoteSnapshotContent>
    {
        public string Value { get; private set; }

        private NoteSnapshotContent() { }
        private NoteSnapshotContent(string value)
        {
            Value = value;
        }

        public static implicit operator string(NoteSnapshotContent title) => title.Value;

        public static NoteSnapshotContent FromString(string title) => new NoteSnapshotContent(title);
    }
}
