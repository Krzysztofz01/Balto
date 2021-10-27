using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Goal
{
    public class GoalStartingDate : ValueObject<GoalStartingDate>
    {
        public DateTime Value { get; private set; }

        private GoalStartingDate() { }
        private GoalStartingDate(DateTime value)
        {
            if (value == default)
                throw new ValueObjectValidationException("The goal starting date can not have the default value.");

            Value = value;
        }

        public static implicit operator DateTime(GoalStartingDate startingDate) => startingDate.Value;

        public static GoalStartingDate Now => new GoalStartingDate(DateTime.Now);
        public static GoalStartingDate FromDateTime(DateTime startingDate) => new GoalStartingDate(startingDate);
    }
}
