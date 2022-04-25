using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Notes
{
    public class NoteOwnerId : Identifier
    {
        private NoteOwnerId() { }
        private NoteOwnerId(Guid value) : base(value) { }

        public static implicit operator string(NoteOwnerId ownerId) => ownerId.Value.ToString();
        public static implicit operator Guid(NoteOwnerId ownerId) => ownerId.Value;

        public static NoteOwnerId FromGuid(Guid guid) => new NoteOwnerId(guid);
        public static NoteOwnerId FromString(string guid) => new NoteOwnerId(Guid.Parse(guid));
    }
}
