using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Projects.ProjectContributors
{
    public class ProjectContributorIdentityId : Identifier
    {
        private ProjectContributorIdentityId() { }
        private ProjectContributorIdentityId(Guid value) : base(value) { }

        public static implicit operator string(ProjectContributorIdentityId contributorId) => contributorId.Value.ToString();
        public static implicit operator Guid(ProjectContributorIdentityId contributorId) => contributorId.Value;

        public static ProjectContributorIdentityId FromGuid(Guid guid) => new ProjectContributorIdentityId(guid);
        public static ProjectContributorIdentityId FromString(string guid) => new ProjectContributorIdentityId(Guid.Parse(guid));
    }
}
