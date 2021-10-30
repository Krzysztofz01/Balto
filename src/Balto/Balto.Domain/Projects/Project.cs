using Balto.Domain.Core.Events;
using Balto.Domain.Core.Model;
using Balto.Domain.Projects.ProjectContributors;
using Balto.Domain.Projects.ProjectTables;
using System;
using System.Collections.Generic;

namespace Balto.Domain.Projects
{
    public class Project : AggregateRoot
    {
        public ProjectTitle Title { get; private set; }
        public ProjectOwnerId OwnerId { get; private set; }
        public ProjectTicketToken TicketToken { get; private set; }

        private readonly List<ProjectContributor> _contributors;
        public IReadOnlyCollection<ProjectContributor> Contributors => _contributors.AsReadOnly();

        private readonly List<ProjectTable> _tables;
        public IReadOnlyCollection<ProjectTable> Tables => _tables.AsReadOnly();

        protected override void Handle(IEventBase @event)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        private Project()
        {
            _contributors = new List<ProjectContributor>();
            _tables = new List<ProjectTable>();
        }

        public static class Factory
        {
            public static Project Create()
            {
                throw new NotImplementedException();
            }
        }
    }
}
