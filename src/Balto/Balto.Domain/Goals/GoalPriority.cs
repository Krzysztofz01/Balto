using Balto.Domain.Shared;

namespace Balto.Domain.Goals
{
    public class GoalPriority : Priority
    {
        private const PriorityTypes _defaultType = PriorityTypes.Medium;

        private GoalPriority() : base() { }
        private GoalPriority(PriorityTypes priorityTypes) : base(priorityTypes) { }

        public static implicit operator PriorityTypes(GoalPriority priority) => priority.Value;
        public static implicit operator string(GoalPriority priority) => priority.GetName();

        public static GoalPriority FromPriorityTypes(PriorityTypes priorityTypes) => new GoalPriority(priorityTypes);
        public static GoalPriority Default => new GoalPriority(_defaultType);
    }
}
