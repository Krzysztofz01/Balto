using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Teams.TeamMembers
{
    public class TeamMemberIdentityId : Identifier
    {
        private TeamMemberIdentityId() { }
        private TeamMemberIdentityId(Guid value) : base(value) { }

        public static implicit operator string(TeamMemberIdentityId contributorId) => contributorId.Value.ToString();
        public static implicit operator Guid(TeamMemberIdentityId contributorId) => contributorId.Value;

        public static TeamMemberIdentityId FromGuid(Guid guid) => new TeamMemberIdentityId(guid);
        public static TeamMemberIdentityId FromString(string guid) => new TeamMemberIdentityId(Guid.Parse(guid));
    }
}
