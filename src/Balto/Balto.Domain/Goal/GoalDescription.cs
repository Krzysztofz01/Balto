using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Goal
{
    public class GoalDescription : ValueObject<GoalDescription>
    {
        private const int _maxLength = 300;

        public string Value { get; private set; }

        private GoalDescription() { }
        private GoalDescription(string value)
        {
            if (!value.IsLengthLess(_maxLength))
                throw new ValueObjectValidationException("Invalid goal description length.");

            Value = value;
        }

        public static implicit operator string(GoalDescription description) => description.Value;

        public static GoalDescription FromString(string description) => new GoalDescription(description);
        public static GoalDescription Empty => new GoalDescription(string.Empty);
    }
}
