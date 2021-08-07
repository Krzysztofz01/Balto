using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Objective
{
    public class ObjectiveEndingDate : Value<ObjectiveEndingDate>
    {
        public DateTime Value { get; internal set; }

        protected ObjectiveEndingDate() { }
        protected ObjectiveEndingDate(DateTime value)
        {
            Validate(value);
            Value = value;
        }

        private static void Validate(DateTime value)
        {
            if (value == default) throw new ArgumentException(nameof(value), "You can not use the default date value.");

            if (value < DateTime.Now) throw new ArgumentException(nameof(value), "You can not set the deadline of a objective in the past.");
        }

        public static implicit operator string(ObjectiveEndingDate date) => date.Value.ToString();
        public static implicit operator DateTime(ObjectiveEndingDate date) => date.Value;

        public static ObjectiveEndingDate Set(DateTime date) => new ObjectiveEndingDate(date);
        public static ObjectiveEndingDate Now => new ObjectiveEndingDate(DateTime.Now);
    }
}
