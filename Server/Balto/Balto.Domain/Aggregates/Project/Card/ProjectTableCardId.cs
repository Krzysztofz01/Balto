using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardId : Value<ProjectTableCardId>
    {
        public Guid Value { get; private set; }

        protected ProjectTableCardId() { }
        public ProjectTableCardId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Card id can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(ProjectTableCardId self) => self.Value;
        public static implicit operator ProjectTableCardId(string value) => new ProjectTableCardId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
