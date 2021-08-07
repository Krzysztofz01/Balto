using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Objective
{
    public class ObjectiveStartingDate : Value<ObjectiveStartingDate>
    {
        public DateTime Value { get; internal set; }

        protected ObjectiveStartingDate() { }
        protected ObjectiveStartingDate(DateTime value)
        {
            Validate(value);
            Value = value;
        }

        private static void Validate(DateTime value)
        {
            if (value == default) throw new ArgumentException(nameof(value), "You can not use the default date value.");

            if (value > DateTime.Now) throw new ArgumentException(nameof(value), "You can not start a objective in the future.");
        }

        public static implicit operator string(ObjectiveStartingDate date) => date.Value.ToString();
        public static implicit operator DateTime(ObjectiveStartingDate date) => date.Value;

        public static ObjectiveStartingDate Set(DateTime date) => new ObjectiveStartingDate(date);
        public static ObjectiveStartingDate Now => new ObjectiveStartingDate(DateTime.Now);
    }
}
