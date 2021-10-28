using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Goals
{
    public class GoalDeadline : ValueObject<GoalDeadline>
    {
        public DateTime? Value { get; private set; }

        private GoalDeadline() { }
        private GoalDeadline(DateTime? value)
        {
            if (value.HasValue)
            {
                if (value.Value == default)
                    throw new ValueObjectValidationException("The goal deadline date can not have the default value");
            }

            Value = value;
        }

        public static implicit operator DateTime?(GoalDeadline deadline) => deadline.Value;

        public static GoalDeadline NoDeadline => new GoalDeadline(null);
        public static GoalDeadline FromDateTime(DateTime deadline) => new GoalDeadline(deadline);
    }
}
