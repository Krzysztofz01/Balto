using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Notes.NoteTags
{
    public class NoteTagId : Identifier
    {
        private NoteTagId() { }
        private NoteTagId(Guid value) : base(value) { }

        public static implicit operator string(NoteTagId contributorId) => contributorId.Value.ToString();
        public static implicit operator Guid(NoteTagId contributorId) => contributorId.Value;

        public static NoteTagId FromGuid(Guid guid) => new NoteTagId(guid);
        public static NoteTagId FromString(string guid) => new NoteTagId(Guid.Parse(guid));
    }
}
