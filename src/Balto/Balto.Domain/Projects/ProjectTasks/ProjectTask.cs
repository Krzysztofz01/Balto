using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;
using Balto.Domain.Projects.ProjectTags;
using System;
using System.Collections.Generic;
using System.Linq;
using static Balto.Domain.Projects.Events;

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

        private readonly List<ProjectTag> _tags;
        public IReadOnlyCollection<ProjectTag> Tags => _tags.SkipDeleted().AsReadOnly();

        protected override void Handle(IEventBase @event)
        {
            switch(@event)
            {
                case V1.ProjectTaskUpdated e: When(e); break;
                case V1.ProjectTaskDeleted e: When(e); break;
                case V1.ProjectTaskStatusChanged e: When(e); break;
                case V1.ProjectTaskOrdinalNumbeChanged e: When(e); break;

                case V1.ProjectTaskTagAssigned e: When(e); break;
                case V1.ProjectTaskTagUnassigned e: When(e); break;
            }
        }

        protected override void Validate()
        {
            bool isNull = Title == null || Content == null || Color == null ||
                CreatorId == null || AssignedContributorId == null || StartingDate == null ||
                Status == null || Priority == null || OrdinalNumber == null;

            if (isNull)
                throw new BusinessLogicException("The project table aggregate properties can not be null.");
        }

        private void When(V1.ProjectTaskOrdinalNumbeChanged @event)
        {
            OrdinalNumber = ProjectTaskOrdinalNumber.FromInt(@event.OrdinalNumber);
        }

        private void When(V1.ProjectTaskStatusChanged @event)
        {
            Status = ProjectTaskStatus.FinishedByContributor(@event.CurrentUserId);
        }

        private void When(V1.ProjectTaskDeleted _)
        {
            DeletedAt = DateTime.Now;
        }

        private void When(V1.ProjectTaskUpdated @event)
        {
            Title = ProjectTaskTitle.FromString(@event.Title);
            Content = ProjectTaskContent.FromString(@event.Content);
            AssignedContributorId = ProjectTaskAssignedContributorId.FromGuid(@event.AssignedContributorId);
            StartingDate = ProjectTaskStartingDate.FromDateTime(@event.StartingDate);
            Deadline = ProjectTaskDeadline.FromDateTime(@event.Deadline);
            Priority = ProjectTaskPriority.FromPriorityTypes(@event.Priority);
        }

        private void When(V1.ProjectTaskTagAssigned @event)
        {
            if (_tags.SkipDeleted().Any(t => t.TagId == @event.TagId))
                throw new BusinessLogicException("This tag is already assigned to this note.");

            _tags.Add(ProjectTag.Factory.Create(@event));
        }

        private void When(V1.ProjectTaskTagUnassigned @event)
        {
            var tag = _tags.SkipDeleted().Single(t => t.TagId.Value == @event.TagId);

            tag.Apply(@event);
        }

        private ProjectTask()
        {
            _tags = new List<ProjectTag>();
        }

        public static class Factory
        {
            public static ProjectTask Create(V1.ProjectTaskCreated @event)
            {
                return new ProjectTask
                {
                    Title = ProjectTaskTitle.FromString(@event.Title),
                    Content = ProjectTaskContent.Empty,
                    Color = ProjectTaskColor.Default,
                    CreatorId = ProjectTaskCreatorId.FromGuid(@event.CurrentUserId),
                    AssignedContributorId = ProjectTaskAssignedContributorId.NoAssignedContributor,
                    StartingDate = ProjectTaskStartingDate.Now,
                    Deadline = ProjectTaskDeadline.NoDeadline,
                    Status = ProjectTaskStatus.Unfinished,
                    Priority = ProjectTaskPriority.Default,
                    OrdinalNumber = ProjectTaskOrdinalNumber.Default
                };
            }

            public static ProjectTask Create(V1.TicketPushed @event)
            {
                return new ProjectTask
                {
                    Title = ProjectTaskTitle.FromString(@event.Title),
                    Content = ProjectTaskContent.Empty,
                    Color = ProjectTaskColor.Default,
                    CreatorId = ProjectTaskCreatorId.Computed,
                    AssignedContributorId = ProjectTaskAssignedContributorId.NoAssignedContributor,
                    StartingDate = ProjectTaskStartingDate.Now,
                    Deadline = ProjectTaskDeadline.NoDeadline,
                    Status = ProjectTaskStatus.Unfinished,
                    Priority = ProjectTaskPriority.Default,
                    OrdinalNumber = ProjectTaskOrdinalNumber.Default
                };
            }
        }
    }
}
