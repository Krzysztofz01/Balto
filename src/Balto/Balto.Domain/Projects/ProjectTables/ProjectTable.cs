using Balto.Domain.Core.Events;
using Balto.Domain.Core.Model;
using Balto.Domain.Projects.ProjectTasks;
using System;
using System.Collections.Generic;

namespace Balto.Domain.Projects.ProjectTables
{
    public class ProjectTable : Entity
    {
        public ProjectTableTitle Title { get; private set; }
        public ProjectTableColor Color { get; private set; }

        private readonly List<ProjectTask> _tasks;
        public IReadOnlyCollection<ProjectTask> Tasks => _tasks.AsReadOnly();

        protected override void Handle(IEventBase @event)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        private ProjectTable()
        {
            _tasks = new List<ProjectTask>();
        }

        public static class Factory
        {
            public static ProjectTable Create()
            {
                throw new NotImplementedException();
            }
        }
    }
}
