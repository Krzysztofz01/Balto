using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Objective
{
    public class ObjectiveFinishDate : Value<ObjectiveFinishDate>
    {
        public DateTime? Value { get; internal set; }

        protected ObjectiveFinishDate() { }
        protected ObjectiveFinishDate(DateTime? value, bool init)
        {
            if (init)
            {
                Value = null;
            }
            else
            {
                Validate(value);
                Value = value;
            }

        }

        private static void Validate(DateTime? value)
        {
            if (value == default) throw new ArgumentException(nameof(value), "You can not use the default date value.");

            if (value is null) throw new ArgumentNullException(nameof(value), "You can not set a null date if the doesnt init.");
        }

        public static implicit operator string(ObjectiveFinishDate date) => date.Value.ToString();
        public static implicit operator DateTime?(ObjectiveFinishDate date ) => date.Value;

        public static ObjectiveFinishDate Set(DateTime date) => new ObjectiveFinishDate(date, false);
        public static ObjectiveFinishDate Unfinished => new ObjectiveFinishDate(null, true);
    }
}
