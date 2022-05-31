using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;
using Balto.Domain.Projects.ProjectTasks;
using System;
using System.Collections.Generic;
using System.Linq;
using static Balto.Domain.Projects.Events;

namespace Balto.Domain.Projects.ProjectTables
{
    public class ProjectTable : Entity
    {
        public ProjectTableTitle Title { get; private set; }
        public ProjectTableColor Color { get; private set; }

        private readonly List<ProjectTask> _tasks;
        public IReadOnlyCollection<ProjectTask> Tasks => _tasks.SkipDeleted().AsReadOnly();

        protected override void Handle(IEventBase @event)
        {
            switch (@event)
            {
                case V1.TicketPushed e: When(e); break;

                case V1.ProjectTableDeleted e: When(e); break;
                case V1.ProjectTableUpdated e: When(e); break;
                case V1.ProjectTableTasksOrdinalNumbersChanged e: When(e); break;

                case V1.ProjectTaskCreated e: When(e); break;
                case V1.ProjectTaskUpdated e: When(e); break;
                case V1.ProjectTaskDeleted e: When(e); break;
                case V1.ProjectTaskStatusChanged e: When(e); break;

                case V1.ProjectTaskTagAssigned e: When(e); break;
                case V1.ProjectTaskTagUnassigned e: When(e); break;

                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = Title == null || Color == null ||
                Tasks == null;

            if (isNull)
                throw new BusinessLogicException("The project table aggregate properties can not be null.");
        }

        private void When(V1.ProjectTableDeleted @event)
        {
            DeletedAt = DateTime.Now;

            foreach(var task in _tasks)
            {
                task.Apply(new V1.ProjectTaskDeleted
                {
                    Id = @event.Id,
                    TableId = @event.TableId,
                    TaskId = task.Id
                });
            }
        }

        private void When(V1.ProjectTableUpdated @event)
        {
            Title = ProjectTableTitle.FromString(@event.Title);
            Color = ProjectTableColor.FromString(@event.Color);
        }

        private void When(V1.ProjectTableTasksOrdinalNumbersChanged @event)
        {
            foreach(var pair in @event.IdOrdinalNumberPairs)
            {
                var task = _tasks
                    .SkipDeleted()
                    .Single(t => t.Id == pair.Item1);

                task.Apply(new V1.ProjectTaskOrdinalNumbeChanged
                {
                    Id = @event.Id,
                    TableId = @event.TableId,
                    TaskId = task.Id,
                    OrdinalNumber = pair.Item2
                });
            }
        }

        private void When(V1.ProjectTaskStatusChanged @event)
        {
            var task = _tasks
                    .SkipDeleted()
                    .Single(t => t.Id == @event.TaskId);

            task.Apply(@event);
        }

        private void When(V1.ProjectTaskDeleted @event)
        {
            var task = _tasks
                    .SkipDeleted()
                    .Single(t => t.Id == @event.TaskId);

            task.Apply(@event);
        }

        private void When(V1.ProjectTaskUpdated @event)
        {
            var task = _tasks
                    .SkipDeleted()
                    .Single(t => t.Id == @event.TaskId);

            task.Apply(@event);
        }

        private void When(V1.ProjectTaskCreated @event)
        {
            var task = ProjectTask.Factory.Create(@event);

            task.Apply(new V1.ProjectTaskOrdinalNumbeChanged
            {
                Id = @event.Id,
                TableId = @event.TableId,
                TaskId = task.Id,
                OrdinalNumber = GetOrdinalNumber()
            });

            _tasks.Add(task);
        }

        private void When(V1.TicketPushed @event)
        {
            var task = ProjectTask.Factory.Create(@event);

            task.Apply(new V1.ProjectTaskOrdinalNumbeChanged
            {
                Id = @event.Id,
                TableId = Id,
                TaskId = task.Id,
                OrdinalNumber = GetOrdinalNumber()
            });

            _tasks.Add(task);
        }

        private void When(V1.ProjectTaskTagAssigned @event)
        {
            var task = _tasks
                    .SkipDeleted()
                    .Single(t => t.Id == @event.TaskId);

            task.Apply(@event);
        }

        private void When(V1.ProjectTaskTagUnassigned @event)
        {
            var task = _tasks
                    .SkipDeleted()
                    .Single(t => t.Id == @event.TaskId);

            task.Apply(@event);
        }

        private int GetOrdinalNumber()
        {
            if (!_tasks.Any())
                return 1;

            return _tasks.Max(t => t.OrdinalNumber.Value) + 1;
        }

        private ProjectTable()
        {
            _tasks = new List<ProjectTask>();
        }

        public static class Factory
        {
            public static ProjectTable Create(V1.ProjectTableCreated @event)
            {
                return new ProjectTable
                {
                    Title = ProjectTableTitle.FromString(@event.Title),
                    Color = ProjectTableColor.Default
                };
            }
        }
    }
}
