using Balto.Domain.Aggregates.Project.Card;
using Balto.Domain.Aggregates.Project.Table;
using Balto.Domain.Common;
using Balto.Domain.Exceptions;
using Balto.Domain.Extensions;
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
        public IReadOnlyCollection<ProjectTable> Tables => _tables.AsReadOnly();

        private const string _ticketTableName = "Tickets";

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
                Title = title,
                CurrentUserId = ownerId
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

        public void ChangeTicketTokenStatus(Guid currentUserId) =>
            Apply(new Events.ProjectTicketStatusChanged
            {
                Id = Id,
                CurrentUserId = currentUserId
            });

        public void AddTicket(string title, string content) =>
            Apply(new Events.ProjectTicketCreated
            {
                Id = Id,
                Title = title,
                Content = content
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

        public void AddCard(Guid tableId, string title, Guid currentUserId) =>
            Apply(new Events.ProjectTableCardCreated
            {
                Id = Id,
                TableId = tableId,
                Title = title,
                CurrentUserId = currentUserId
            });

        public void UpdateCard(Guid cardId, string title, string content, string color, DateTime startingDate, bool notify, DateTime? endingDate, Guid? assginedUserId, CardPriorityType priority) =>
            Apply(new Events.ProjectTableCardUpdated
            {
                Id = Id,
                CardId = cardId,
                Title = title,
                Content = content,
                Color = color,
                StartingDate = startingDate,
                Notify = notify,
                EndingDate = endingDate,
                AssignedUserId = assginedUserId,
                Priority = priority
            });

        public void DeleteCard(Guid cardId, Guid currentUserId) =>
            Apply(new Events.ProjectTableCardDeleted
            {
                Id = Id,
                CardId = cardId,
                CurrentUserId = currentUserId
            });

        public void ChangeCardStatus(Guid cardId, Guid currentUserId) =>
            Apply(new Events.ProjectTableCardStatusChanged
            {
                Id = Id,
                CardId = cardId,
                CurrentUserId = currentUserId
            });

        public void AddCommentToCard(Guid cardId, string content, Guid currentUserId) =>
            Apply(new Events.ProjectTableCardCommentCreated
            {
                Id =Id,
                CardId = cardId,
                CurrentUserId = currentUserId,
                Content = content
            });

        public void DeleteCommentFromCard(Guid commentId, Guid currentUserId) =>
            Apply(new Events.ProjectTableCardCommentDeleted
            {
                Id = Id,
                CommentId = commentId,
                CurrentUserId = currentUserId
            });

        //Aggregate root abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.ProjectCreated e:
                    Id = new ProjectId(e.Id);
                    OwnerId = new ProjectOwnerId(e.CurrentUserId);
                    Title = ProjectTitle.FromString(e.Title);
                    TicketToken = ProjectTicketToken.Empty;
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

                case Events.ProjectTicketStatusChanged e:
                    ValidateAccess(e.CurrentUserId);

                    TicketToken = TicketToken.Value.IsEmpty() ? ProjectTicketToken.Generate() : ProjectTicketToken.Empty;
                    break;

                case Events.ProjectTicketCreated e:
                    var ticketTable = GetOrCreateTicketTable();

                    ApplyToEntity(ticketTable, e);
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

                case Events.ProjectTableCardCreated e:
                    var targetTableToAddCard = _tables.Single(t => t.Id.Value == e.TableId);
                    ApplyToEntity(targetTableToAddCard, e);
                    break;

                case Events.ProjectTableCardUpdated e:
                    var targetTableToUpdateCard = _tables.Single(t => t.Cards.Any(c => c.Id.Value == e.CardId));
                    ApplyToEntity(targetTableToUpdateCard, e);
                    break;

                case Events.ProjectTableCardDeleted e:
                    var targetTableToDeleteCard = _tables.Single(t => t.Cards.Any(c => c.Id.Value == e.CardId));
                    var targetCardAuthorId = targetTableToDeleteCard.Cards.Single(c => c.Id.Value == e.CardId).CreatorId.Value;

                    if (e.CurrentUserId != targetCardAuthorId) ValidateAccess(e.CurrentUserId);

                    ApplyToEntity(targetTableToDeleteCard, e);
                    break;

                case Events.ProjectTableCardStatusChanged e:
                    var targetTableToChangeCardStatus = _tables.Single(t => t.Cards.Any(c => c.Id.Value == e.CardId));
                    ApplyToEntity(targetTableToChangeCardStatus, e);
                    break;

                case Events.ProjectTableCardCommentCreated e:
                    var targetTableToAddCardComment = _tables.Single(t => t.Cards.Any(c => c.Id.Value == e.CardId));
                    ApplyToEntity(targetTableToAddCardComment, e);
                    break;

                case Events.ProjectTableCardCommentDeleted e:
                    var targetTableToDeleteCardComment = _tables.Single(t => t.Cards.Any(c => c.Comments.Any(m => m.Id.Value == e.CommentId)));
                    ApplyToEntity(targetTableToDeleteCardComment, e);
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

        private ProjectTable GetOrCreateTicketTable()
        {
            var ticketTable = _tables.SingleOrDefault(t => t.Title == _ticketTableName);

            if (ticketTable is null)
            {
                ticketTable = new ProjectTable(Apply);

                ApplyToEntity(ticketTable, new Events.ProjectTableCreated
                {
                    Id = Id,
                    Title = _ticketTableName
                });

                _tables.Add(ticketTable);

                ticketTable = _tables.Single(t => t.Title == _ticketTableName);
            }

            return ticketTable;
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
