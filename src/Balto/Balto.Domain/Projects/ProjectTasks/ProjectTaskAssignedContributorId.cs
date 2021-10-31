using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTaskAssignedContributorId : UnrestrictedIdentifier
    {
        private ProjectTaskAssignedContributorId() { }
        private ProjectTaskAssignedContributorId(Guid? value) : base(value) { }

        public static implicit operator Guid?(ProjectTaskAssignedContributorId teamId) => teamId.Value;

        public static ProjectTaskAssignedContributorId FromString(string guid) => new ProjectTaskAssignedContributorId(Guid.Parse(guid));
        public static ProjectTaskAssignedContributorId FromGuid(Guid guid) => new ProjectTaskAssignedContributorId(guid);
        public static ProjectTaskAssignedContributorId NoAssignedContributor => new ProjectTaskAssignedContributorId(null);
    }
}
