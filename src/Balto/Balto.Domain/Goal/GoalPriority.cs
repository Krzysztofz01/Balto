using Balto.Domain.Shared;

namespace Balto.Domain.Goal
{
    public class GoalPriority : Priority
    {
        private GoalPriority() : base() { }
        private GoalPriority(PriorityTypes priorityTypes) : base(priorityTypes) { }

        public static implicit operator PriorityTypes(GoalPriority priority) => priority.Value;
        public static implicit operator string(GoalPriority priority) => priority.GetName();

        public static GoalPriority FromPriorityTypes(PriorityTypes priorityTypes) => new GoalPriority(priorityTypes);
    }
}
