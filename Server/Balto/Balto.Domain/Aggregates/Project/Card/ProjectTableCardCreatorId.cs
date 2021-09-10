using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardCreatorId : Value<ProjectTableCardCreatorId>
    {
        public Guid Value { get; private set; }

        protected ProjectTableCardCreatorId() { }
        public ProjectTableCardCreatorId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "User id value can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(ProjectTableCardCreatorId self) => self.Value;
        public static ProjectTableCardCreatorId NoUser => new ProjectTableCardCreatorId();
    }
}
