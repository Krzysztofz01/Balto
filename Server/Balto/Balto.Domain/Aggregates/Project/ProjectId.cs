using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project
{
    public class ProjectId : Value<ProjectId>
    {
        public Guid Value { get; private set; }

        protected ProjectId() { }
        public ProjectId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Project id can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(ProjectId self) => self.Value;
        public static implicit operator ProjectId(string value) => new ProjectId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
