using Balto.Domain.Aggregates.Project.Card;
using Balto.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Balto.Domain.Aggregates.Project.Table
{
    public class ProjectTable : Entity<ProjectTableId>
    {
        //Persistence
        public Guid TableId { get; private set; }

        //Properties
        public ProjectTableTitle Title { get; private set; }
        public ProjectTableColor Color { get; private set; }

        private readonly List<ProjectTableCard> _cards;
        public IReadOnlyCollection<ProjectTableCard> Cards => _cards.AsReadOnly();


        //Constructors
        protected ProjectTable()
        {
            _cards = new List<ProjectTableCard>();
        }

        public ProjectTable(Action<object> applier) : base(applier)
        {
            _cards = new List<ProjectTableCard>();
        }

        //Entity abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.ProjectTableCreated e:
                    Id = new ProjectTableId(Guid.NewGuid());
                    Title = ProjectTableTitle.FromString(e.Title);
                    Color = ProjectTableColor.Default;
                    break;

                case Events.ProjectTableUpdated e:
                    Title = ProjectTableTitle.FromString(e.Title);
                    Color = ProjectTableColor.Set(e.Color);
                    break;

                case Events.ProjectTableCardCreated e:
                    var cardToCreate = new ProjectTableCard(Apply);
                    ApplyToEntity(cardToCreate, e);

                    _cards.Add(cardToCreate);
                    break;

                case Events.ProjectTicketCreated e:
                    var ticketCardToCreate = new ProjectTableCard(Apply);
                    ApplyToEntity(ticketCardToCreate, e);

                    _cards.Add(ticketCardToCreate);
                    break;

                case Events.ProjectTableCardUpdated e:
                    var cardToUpdate = _cards.Single(c => c.Id.Value == e.CardId);
                    ApplyToEntity(cardToUpdate, e);
                    break;

                case Events.ProjectTableCardDeleted e:
                    var cardToDelete = _cards.Single(c => c.Id.Value == e.CardId);

                    _cards.Remove(cardToDelete);
                    break;

                case Events.ProjectTableCardStatusChanged e:
                    var cardToChangeStatus = _cards.Single(c => c.Id.Value == e.CardId);
                    ApplyToEntity(cardToChangeStatus, e);
                    break;

                case Events.ProjectTableCardCommentCreated e:
                    var cardToAddComment = _cards.Single(c => c.Id.Value == e.CardId);
                    ApplyToEntity(cardToAddComment, e);
                    break;

                case Events.ProjectTableCardCommentDeleted e:
                    var cardWithCommentToDelete = _cards.Single(c => c.Comments.Any(m => m.Id.Value == e.CommentId));
                    ApplyToEntity(cardWithCommentToDelete, e);
                    break;
            }
        }
    }
}
