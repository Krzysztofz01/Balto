using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project
{
    public class ProjectOwnerId : Value<ProjectOwnerId>
    {
        public Guid Value { get; private set; }

        protected ProjectOwnerId() { }
        public ProjectOwnerId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "User id value can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(ProjectOwnerId self) => self.Value;
        public static ProjectOwnerId NoUser => new ProjectOwnerId();
    }
}
