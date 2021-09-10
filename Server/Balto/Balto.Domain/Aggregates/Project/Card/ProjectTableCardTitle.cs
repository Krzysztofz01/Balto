using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardTitle : Value<ProjectTableCardTitle>
    {
        public string Value { get; private set; }

        protected ProjectTableCardTitle() { }
        protected ProjectTableCardTitle(string value)
        {
            Validate(value);

            Value = value;
        }

        private static void Validate(string value)
        {
            if (value.Length > 100) throw new ArgumentOutOfRangeException(nameof(value), "Project table card title can not be longer than 100 characters.");
        }

        public static implicit operator string(ProjectTableCardTitle title) => title.Value;

        public static ProjectTableCardTitle FromString(string value) => new ProjectTableCardTitle(value);
    }
}
