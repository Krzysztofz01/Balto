using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;
using Balto.Domain.Projects.ProjectContributors;
using Balto.Domain.Projects.ProjectTables;
using System;
using System.Collections.Generic;
using System.Linq;
using static Balto.Domain.Projects.Events;

namespace Balto.Domain.Projects
{
    public class Project : AggregateRoot
    {
        private const string _ticketTableName = "Tickets";

        public ProjectTitle Title { get; private set; }
        public ProjectOwnerId OwnerId { get; private set; }
        public ProjectTicketToken TicketToken { get; private set; }

        private readonly List<ProjectContributor> _contributors;
        public IReadOnlyCollection<ProjectContributor> Contributors => _contributors.SkipDeleted().AsReadOnly();

        private readonly List<ProjectTable> _tables;
        public IReadOnlyCollection<ProjectTable> Tables => _tables.SkipDeleted().AsReadOnly();

        protected override void Handle(IEventBase @event)
        {
            switch(@event)
            {
                case V1.ProjectUpdated e: When(e); break;
                case V1.ProjectDeleted e: When(e); break;
                
                case V1.TicketPushed e: When(e); break;
                
                case V1.ProjectContributorAdded e: When(e); break;
                case V1.ProjectContributorDeleted e: When(e); break;
                case V1.ProjectContributorUpdated e: When(e); break;
                case V1.ProjectContributorLeft e: When(e); break;

                case V1.ProjectTableCreated e: When(e); break;
                case V1.ProjectTableDeleted e: When(e); break;
                case V1.ProjectTableUpdated e: When(e); break;
                case V1.ProjectTableTasksOrdinalNumbersChanged e: When(e); break;

                case V1.ProjectTaskCreated e: When(e); break;
                case V1.ProjectTaskUpdated e: When(e); break;
                case V1.ProjectTaskDeleted e: When(e); break;
                case V1.ProjectTaskStatusChanged e: When(e); break;

                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = Title == null || OwnerId == null ||
                TicketToken == null || Contributors == null || Tables == null;

            if (isNull)
                throw new BusinessLogicException("The project aggregate properties can not be null.");
        }

        private void When(V1.ProjectUpdated @event)
        {
            CheckIfOwner(@event);

            Title = ProjectTitle.FromString(@event.Title);

            if (@event.TicketStatus.HasValue)
            {
                TicketToken = @event.TicketStatus.Value ?
                    ProjectTicketToken.Enable : ProjectTicketToken.Disable;
            }
        }

        private void When(V1.ProjectDeleted @event)
        {
            CheckIfOwner(@event);

            DeletedAt = DateTime.Now;

            foreach(var table in _tables)
            {
                table.Apply(new V1.ProjectTableDeleted
                {
                    Id = @event.Id,
                    CurrentUserId = @event.CurrentUserId,
                    TableId = table.Id
                });
            }
        }

        private void When(V1.TicketPushed @event)
        {
            if (!_tables.Any(t => t.Title == _ticketTableName))
            {
                _tables.Add(ProjectTable.Factory.Create(new V1.ProjectTableCreated
                {
                    Id = @event.Id,
                    Title = _ticketTableName
                }));
            }

            var table = _tables.Single(t => t.Title == _ticketTableName);

            table.Apply(@event);
        }

        private void When(V1.ProjectContributorAdded @event)
        {
            CheckIfOwner(@event);

            if (@event.UserId == OwnerId)
                throw new InvalidOperationException("The owner is already a contributor.");

            var contributor = _contributors.SingleOrDefault(c => c.IdentityId.Value == @event.UserId);
            if (contributor != null)
            {
                if (contributor.DeletedAt is null)
                    throw new InvalidOperationException("This identity is already a contributor.");

                if (contributor.DeletedAt is not null)
                    _contributors.Remove(contributor);
            }

            _contributors.Add(ProjectContributor.Factory.Create(@event));
        }

        private void When(V1.ProjectContributorDeleted @event)
        {
            CheckIfOwner(@event);

            var contributor = _contributors
                .SkipDeleted()
                .Single(c => c.IdentityId.Value == @event.UserId);

            contributor.Apply(@event);
        }

        private void When(V1.ProjectContributorUpdated @event)
        {
            CheckIfOwner(@event);

            var contributor = _contributors
                .SkipDeleted()
                .Single(c => c.IdentityId.Value == @event.UserId);

            contributor.Apply(@event);
        }

        private void When(V1.ProjectContributorLeft @event)
        {
            if (OwnerId == @event.CurrentUserId)
                throw new InvalidOperationException("The owner can not leave the project.");

            var contributor = _contributors
                .SkipDeleted()
                .Single(c => c.IdentityId.Value == @event.CurrentUserId);

            contributor.Apply(new V1.ProjectContributorDeleted
            {
                CurrentUserId = @event.CurrentUserId,
                Id = @event.Id,
                UserId = @event.CurrentUserId
            });
        }

        private void When(V1.ProjectTableCreated @event)
        {
            if (_ticketTableName == @event.Title)
                throw new InvalidOperationException("This table name is reserved.");

            _tables.Add(ProjectTable.Factory.Create(@event));
        }

        private void When(V1.ProjectTableDeleted @event)
        {
            CheckIfOwnerOrManager(@event);

            var table = _tables
                .SkipDeleted()
                .Single(t => t.Id == @event.TableId);

            table.Apply(@event);
        }

        private void When(V1.ProjectTableUpdated @event)
        {
            if (_ticketTableName == @event.Title)
                throw new InvalidOperationException("This table name is reserved.");

            var table = _tables
                .SkipDeleted()
                .Single(t => t.Id == @event.TableId);

            table.Apply(@event);
        }

        private void When(V1.ProjectTableTasksOrdinalNumbersChanged @event)
        {
            var table = _tables
                .SkipDeleted()
                .Single(t => t.Id == @event.TableId);

            table.Apply(@event);

        }

        private void When(V1.ProjectTaskStatusChanged @event)
        {
            var table = _tables
                .SkipDeleted()
                .Single(t => t.Id == @event.TableId);

            table.Apply(@event);
        }

        private void When(V1.ProjectTaskDeleted @event)
        {
            var table = _tables
                .SkipDeleted()
                .Single(t => t.Id == @event.TableId);

            table.Apply(@event);
        }

        private void When(V1.ProjectTaskUpdated @event)
        {
            var table = _tables
                .SkipDeleted()
                .Single(t => t.Id == @event.TableId);

            table.Apply(@event);
        }

        private void When(V1.ProjectTaskCreated @event)
        {
            var table = _tables
                .SkipDeleted()
                .Single(t => t.Id == @event.TableId);

            table.Apply(@event);
        }

        private void CheckIfOwner(IAuthorizableEvent @event)
        {
            if (@event.CurrentUserId != OwnerId)
                throw new InvalidOperationException("No permission to perform this operation.");
        }

        private void CheckIfOwnerOrManager(IAuthorizableEvent @event)
        {
            if (@event.CurrentUserId != OwnerId)
                throw new InvalidOperationException("No permission to perform this operation.");

            if (!_contributors.SkipDeleted().Any(c => c.IdentityId.Value == @event.CurrentUserId && c.Role == ContributorRole.Manager))
                throw new InvalidOperationException("No permission to perform this operation.");
        }

        private Project()
        {
            _contributors = new List<ProjectContributor>();
            _tables = new List<ProjectTable>();
        }

        public static class Factory
        {
            public static Project Create(V1.ProjectCreated @event)
            {
                return new Project
                {
                    Title = ProjectTitle.FromString(@event.Title),
                    OwnerId = ProjectOwnerId.FromGuid(@event.CurrentUserId),
                    TicketToken = ProjectTicketToken.Disable
                };
            }
        }
    }
}
