using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Projects.ProjectContributors
{
    public class ProjectContributorRole : ValueObject<ProjectContributorRole>
    {
        public ContributorRole Value { get; private set; }

        private ProjectContributorRole() { }
        private ProjectContributorRole(ContributorRole value)
        {
            Value = value;
        }

        public static implicit operator ContributorRole(ProjectContributorRole contributorRole) => contributorRole.Value;
        public static implicit operator string(ProjectContributorRole contributorRole) => Enum.GetName(typeof(ContributorRole), contributorRole.Value);

        public static ProjectContributorRole Default => new ProjectContributorRole(ContributorRole.Default);
        public static ProjectContributorRole FromContributorRole(ContributorRole role) => new ProjectContributorRole(role);
    }

    public enum ContributorRole
    {
        Default,
        Manager
    }
}
