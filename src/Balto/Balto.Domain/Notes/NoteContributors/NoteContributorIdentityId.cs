using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Notes.NoteContributors
{
    public class NoteContributorIdentityId : Identifier
    {
        private NoteContributorIdentityId() { }
        private NoteContributorIdentityId(Guid value) : base(value) { }

        public static implicit operator string(NoteContributorIdentityId contributorId) => contributorId.Value.ToString();
        public static implicit operator Guid(NoteContributorIdentityId contributorId) => contributorId.Value;

        public static NoteContributorIdentityId FromGuid(Guid guid) => new NoteContributorIdentityId(guid);
        public static NoteContributorIdentityId FromString(string guid) => new NoteContributorIdentityId(Guid.Parse(guid));
    }
}
