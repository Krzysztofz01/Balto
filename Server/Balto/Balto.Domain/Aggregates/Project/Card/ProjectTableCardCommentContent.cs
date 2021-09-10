using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardCommentContent : Value<ProjectTableCardCommentContent>
    {
        public string Value { get; private set; }

        protected ProjectTableCardCommentContent() { }
        protected ProjectTableCardCommentContent(string value)
        {
            Validate(value);

            Value = value;
        }

        private static void Validate(string value)
        {
            if (value.Length > 200) throw new ArgumentOutOfRangeException(nameof(value), "Project table card comment content can not be longer than 200 characters.");
        }

        public static implicit operator string(ProjectTableCardCommentContent title) => title.Value;

        public static ProjectTableCardCommentContent FromString(string value) => new ProjectTableCardCommentContent(value);
    }
}
