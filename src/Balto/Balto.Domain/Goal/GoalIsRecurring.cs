using Balto.Domain.Core.Model;

namespace Balto.Domain.Goal
{
    public class GoalIsRecurring : ValueObject<GoalIsRecurring>
    {
        public bool Value { get; private set; }

        private GoalIsRecurring() { }
        private GoalIsRecurring(bool value)
        {
            Value = value;
        }

        public static implicit operator bool(GoalIsRecurring isRecurring) => isRecurring.Value;

        public static GoalIsRecurring FromBool(bool isRecurring) => new GoalIsRecurring(isRecurring);
    }
}
