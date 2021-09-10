using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardContent : Value<ProjectTableCardContent>
    {
        public string Value { get; private set; }

        protected ProjectTableCardContent() { }
        protected ProjectTableCardContent(string value)
        {
            Validate(value);

            Value = value;
        }

        private static void Validate(string value)
        {
            if (value.Length > 2000) throw new ArgumentOutOfRangeException(nameof(value), "Project table card content is too log.");
        }

        public static implicit operator string(ProjectTableCardContent content) => content.Value;

        public static ProjectTableCardContent FromString(string value) => new ProjectTableCardContent(value);
    }
}
