using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardCommentCreateDate : Value<ProjectTableCardCommentCreateDate>
    {
        public DateTime Value { get; private set; }

        protected ProjectTableCardCommentCreateDate() { }
        protected ProjectTableCardCommentCreateDate(DateTime value) => Value = value;

        public static implicit operator DateTime(ProjectTableCardCommentCreateDate self) => self.Value;

        public static ProjectTableCardCommentCreateDate Now => new ProjectTableCardCommentCreateDate(DateTime.Now);
    }
}
