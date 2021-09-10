using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardStartingDate : Value<ProjectTableCardStartingDate>
    {
        public DateTime Value { get; internal set; }

        protected ProjectTableCardStartingDate() { }
        protected ProjectTableCardStartingDate(DateTime value)
        {
            Validate(value);
            Value = value;
        }

        private static void Validate(DateTime value)
        {
            if (value == default) throw new ArgumentException(nameof(value), "You can not use the default date value.");

            if (value > DateTime.Now) throw new ArgumentException(nameof(value), "You can not start a project task card in the future.");
        }

        public static implicit operator string(ProjectTableCardStartingDate date) => date.Value.ToString();
        public static implicit operator DateTime(ProjectTableCardStartingDate date) => date.Value;

        public static ProjectTableCardStartingDate Set(DateTime date) => new ProjectTableCardStartingDate(date);
        public static ProjectTableCardStartingDate Now => new ProjectTableCardStartingDate(DateTime.Now);
    }
}
