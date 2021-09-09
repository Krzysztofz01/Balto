using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project
{
    public class ProjectContributorId : Value<ProjectContributorId>
    {
        public Guid Value { get; private set; }

        protected ProjectContributorId() { }
        public ProjectContributorId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "User id value can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(ProjectContributorId self) => self.Value;
        public static ProjectContributorId NoUser => new ProjectContributorId();
    }
}
