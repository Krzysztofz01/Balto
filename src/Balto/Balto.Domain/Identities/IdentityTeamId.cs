using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Identities
{
    [Obsolete]
    public class IdentityTeamId : UnrestrictedIdentifier
    {
        private IdentityTeamId() { }
        private IdentityTeamId(Guid? value) : base(value) { }

        public static implicit operator Guid?(IdentityTeamId teamId) => teamId.Value;

        public static IdentityTeamId FromString(string guid) => new IdentityTeamId(Guid.Parse(guid));
        public static IdentityTeamId FromGuid(Guid guid) => new IdentityTeamId(guid);
        public static IdentityTeamId NoTeam => new IdentityTeamId(null);
    }
}
