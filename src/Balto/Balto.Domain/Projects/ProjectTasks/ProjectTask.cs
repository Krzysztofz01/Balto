using Balto.Domain.Core.Events;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTask : Entity
    {
        public ProjectTaskTitle Title { get; private set; }
        public ProjectTaskContent Content { get; private set; }
        public ProjectTaskColor Color { get; private set; }
        public ProjectTaskCreatorId CreatorId { get; private set; }
        public ProjectTaskAssignedContributorId AssignedContributorId { get; private set; }
        public ProjectTaskStartingDate StartingDate { get; private set; }
        public ProjectTaskDeadline Deadline { get; private set; }
        public ProjectTaskStatus Status { get; private set; }
        public ProjectTaskPriority Priority { get; private set; }
        public ProjectTaskOrdinalNumber OrdinalNumber { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        private ProjectTask() { }

        public static class Factory
        {
            public static ProjectTask Create()
            {
                throw new NotImplementedException();
            }
        }
    }
}
