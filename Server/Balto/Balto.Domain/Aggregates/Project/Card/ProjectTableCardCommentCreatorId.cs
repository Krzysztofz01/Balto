using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardCommentCreatorId : Value<ProjectTableCardCommentCreatorId>
    {
        public Guid Value { get; private set; }

        protected ProjectTableCardCommentCreatorId() { }
        public ProjectTableCardCommentCreatorId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "User id value can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(ProjectTableCardCommentCreatorId self) => self.Value;
        public static ProjectTableCardCommentCreatorId NoUser => new ProjectTableCardCommentCreatorId();
    }
}
