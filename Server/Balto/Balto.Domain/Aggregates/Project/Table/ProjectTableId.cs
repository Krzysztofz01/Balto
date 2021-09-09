using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Table
{
    public class ProjectTableId : Value<ProjectTableId>
    {
        public Guid Value { get; private set; }

        protected ProjectTableId() { }
        public ProjectTableId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Table id can not be empty.");

            Value = value;
        }
        
        public static implicit operator Guid(ProjectTableId self) => self.Value;
        public static implicit operator ProjectTableId(string value) => new ProjectTableId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
