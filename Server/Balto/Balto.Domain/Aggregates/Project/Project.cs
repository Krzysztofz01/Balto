using Balto.Domain.Aggregates.Project.Table;
using Balto.Domain.Common;
using Balto.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Balto.Domain.Aggregates.Project
{
    public class Project : AggregateRoot<ProjectId>
    {
        //Persistence
        public Guid ProjectId { get; private set; }

        //Properties
        public ProjectTitle Title { get; private set; }
        public ProjectOwnerId OwnerId { get; private set; }
        public ProjectTicketToken TicketToken { get; private set; }

        private readonly List<ProjectContributor> _contributors;
        public IReadOnlyCollection<ProjectContributor> Contributors { get; private set; }

        private readonly List<ProjectTable> _tables;
        public IReadOnlyCollection<ProjectTable> Tables { get; private set; }


        //Constructors
        protected Project()
        {
            _contributors = new List<ProjectContributor>();
            _tables = new List<ProjectTable>();
        }

        protected Project(ProjectOwnerId ownerId, ProjectTitle title)
        {
            _contributors = new List<ProjectContributor>();
            _tables = new List<ProjectTable>();

            Apply(new Events.ProjectCreated
            {
                Id = Guid.NewGuid(),
                Title = title
            });
        }


        //Methods
        public void Update(string title, Guid currentUserId) =>
            Apply(new Events.ProjectUpdated
            {
                Id = Id,
                Title = title,
                CurrentUserId = currentUserId
            });

        public void Delete(Guid currentUserId) =>
            Apply(new Events.ProjectDeleted
            {
                Id = Id,
                CurrentUserId = currentUserId
            });

        public void AddContributor(Guid contributorId, Guid currentUserId) =>
            Apply(new Events.ProjectContributorAdded
            {
                Id = Id,
                ContributorId = contributorId,
                CurrentUserId = currentUserId
            });

        public void DeleteContributor(Guid contributorId, Guid currentUserId) =>
            Apply(new Events.ProjectContributorDeleted
            {
                Id = Id,
                ContributorId = contributorId,
                CurrentUserId = currentUserId
            });

        public void Leave(Guid currentUserId) =>
            Apply(new Events.ProjectLeave
            {
                Id = Id,
                CurrentUserId = currentUserId
            });

        public void AddTable(string title, Guid currentUserId) =>
            Apply(new Events.ProjectTableCreated
            {
                Id = Id,
                CurrentUserId = currentUserId,
                Title = title
            });

        public void UpdateTable(Guid tableId, string title, string color, Guid currentUserId) =>
            Apply(new Events.ProjectTableUpdated
            {
                Id = Id,
                TableId = tableId,
                Title = title,
                Color = color,
                CurrentUserId = currentUserId
            });

        public void DeleteTable(Guid tableId, Guid currentUserId) =>
            Apply(new Events.ProjectTableDeleted
            {
                Id = Id,
                TableId = tableId,
                CurrentUserId = currentUserId
            });

        //Aggregate root abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.ProjectCreated e:
                    Id = new ProjectId(e.Id);
                    OwnerId = new ProjectOwnerId(e.Id);
                    Title = ProjectTitle.FromString(e.Title);
                    break;

                case Events.ProjectUpdated e:
                    ValidateAccess(e.CurrentUserId);

                    Title = ProjectTitle.FromString(e.Title);
                    break;

                case Events.ProjectDeleted e:
                    ValidateAccess(e.CurrentUserId);

                    SetAsDeleted();
                    break;

                case Events.ProjectContributorAdded e:
                    ValidateAccess(e.CurrentUserId);

                    var contributor = new ProjectContributor(Apply);
                    ApplyToEntity(contributor, e);

                    _contributors.Add(contributor);
                    break;

                case Events.ProjectContributorDeleted e:
                    ValidateAccess(e.CurrentUserId);

                    var targetUserForDelete = _contributors.Single(u => u.Id.Value == e.ContributorId);
                    _contributors.Remove(targetUserForDelete);
                    break;

                case Events.ProjectLeave e:
                    if (e.CurrentUserId == OwnerId.Value)
                        throw new InvalidOperationException("The project owner can not leave the project.");

                    var targetUserForLeave = _contributors.Single(u => u.Id.Value == e.CurrentUserId);
                    _contributors.Remove(targetUserForLeave);
                    break;

                case Events.ProjectTableCreated e:
                    ValidateAccess(e.CurrentUserId);

                    var table = new ProjectTable(Apply);
                    ApplyToEntity(table, e);

                    _tables.Add(table);
                    break;

                case Events.ProjectTableUpdated e:
                    ValidateAccess(e.CurrentUserId);

                    var targetTableToUpdate = _tables.Single(t => t.Id.Value == e.TableId);
                    ApplyToEntity(targetTableToUpdate, e);
                    break;

                case Events.ProjectTableDeleted e:
                    ValidateAccess(e.CurrentUserId);

                    var targetTableToDelete = _tables.Single(t => t.Id.Value == e.TableId);
                    _tables.Remove(targetTableToDelete);
                    break;
            }
        }

        protected override void EnsureValidState()
        {
            //Null check
            bool valid = Id != null && OwnerId != null &&
                _contributors != null && _tables != null && Title != null && TicketToken != null;

            if (!valid)
                throw new InvalidEntityStateException(this, "Final property validation failed.");
        }

        private void ValidateAccess(Guid userId)
        {
            if (userId != OwnerId.Value)
                throw new UnauthorizedAccessException("Current user have no permission to perform this operation.");
        }


        //Factory
        public static class Factory
        {
            public static Project Create(ProjectOwnerId ownerId, ProjectTitle title)
            {
                return new Project(ownerId, title);
            }
        }
    }
}
