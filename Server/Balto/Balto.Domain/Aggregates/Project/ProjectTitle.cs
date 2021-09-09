using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project
{
    public class ProjectTitle : Value<ProjectTitle>
    {
        public string Value { get; private set; }

        protected ProjectTitle() { }
        protected ProjectTitle(string value)
        {
            Validate(value);

            Value = value;
        }

        private static void Validate(string value)
        {
            if (value.Length > 100) throw new ArgumentOutOfRangeException(nameof(value), "Project title can not be longer than 100 characters.");
        }

        public static implicit operator string(ProjectTitle title) => title.Value;
        public static ProjectTitle FromString(string value) => new ProjectTitle(value);
    }
}
