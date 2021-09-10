using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardCommentId : Value<ProjectTableCardCommentId>
    {
        public Guid Value { get; private set; }

        protected ProjectTableCardCommentId() { }
        public ProjectTableCardCommentId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Card comment id can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(ProjectTableCardCommentId self) => self.Value;
        public static implicit operator ProjectTableCardCommentId(string value) => new ProjectTableCardCommentId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
