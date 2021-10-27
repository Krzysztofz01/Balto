using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Goal
{
    public class GoalTitle : ValueObject<GoalTitle>
    {
        private const int _maxLength = 30;

        public string Value { get; private set; }

        private GoalTitle() { }
        private GoalTitle(string value)
        {
            if (value.IsEmpty() && !value.IsLengthLess(_maxLength))
                throw new ValueObjectValidationException("Invalid goal title length.");

            Value = value;
        }

        public static implicit operator string(GoalTitle title) => title.Value;

        public static GoalTitle FromString(string title) => new GoalTitle(title);
    }
}
