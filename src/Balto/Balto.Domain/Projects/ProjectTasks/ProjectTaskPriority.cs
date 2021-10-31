using Balto.Domain.Shared;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTaskPriority : Priority
    {
        private const PriorityTypes _defaultType = PriorityTypes.Medium;

        private ProjectTaskPriority() : base() { }
        private ProjectTaskPriority(PriorityTypes priorityTypes) : base(priorityTypes) { }

        public static implicit operator PriorityTypes(ProjectTaskPriority priority) => priority.Value;
        public static implicit operator string(ProjectTaskPriority priority) => priority.GetName();

        public static ProjectTaskPriority FromPriorityTypes(PriorityTypes priorityTypes) => new ProjectTaskPriority(priorityTypes);
        public static ProjectTaskPriority Default => new ProjectTaskPriority(_defaultType);
    }
}
