using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Objective
{
    public class ObjectiveTitle : Value<ObjectiveTitle>
    {
        public string Value { get; internal set; }

        protected ObjectiveTitle() { }
        protected ObjectiveTitle(string value) => Value = value;

        private static void Validate(string value)
        {
            if (value.Length > 100) throw new ArgumentOutOfRangeException(nameof(value), "Objective title can not be longer than 100 characters.");
        }

        public static implicit operator string(ObjectiveTitle title) => title.Value;

        public static ObjectiveTitle FromString(string title)
        {
            Validate(title);
            return new ObjectiveTitle(title);
        }

        public static ObjectiveTitle NoTitle => new ObjectiveTitle();
        
    }
}
