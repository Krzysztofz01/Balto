using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Table
{
    public class ProjectTableTitle : Value<ProjectTableTitle>
    {
        public string Value { get; private set; }

        protected ProjectTableTitle() { }
        protected ProjectTableTitle(string value)
        {
            Validate(value);

            Value = value;
        }

        private static void Validate(string value)
        {
            if (value.Length > 100) throw new ArgumentOutOfRangeException(nameof(value), "Project table title can not be longer than 100 characters.");
        }

        public static implicit operator string(ProjectTableTitle title) => title.Value;

        public static ProjectTableTitle FromString(string value) => new ProjectTableTitle(value);
    }
}
