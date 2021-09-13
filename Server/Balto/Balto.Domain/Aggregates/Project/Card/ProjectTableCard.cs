using Balto.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCard : Entity<ProjectTableCardId>
    {
        //Persistence
        public Guid CardId { get; private set; }

        //Properties
        public ProjectTableCardTitle Title { get; private set; }
        public ProjectTableCardContent Content { get; private set; }
        public ProjectTableCardColor Color { get; private set; }
        public ProjectTableCardCreatorId CreatorId { get; private set; }
        public ProjectTableCardStartingDate StartingDate { get; private set; }
        public ProjectTableCardDeadline Deadline { get; private set; }
        public ProjectTableCardFinished Finished { get; private set; }
        public ProjectTableCardPriority Priority { get; private set; }
        public int OrdinalNumber { get; private set; }

        private readonly List<ProjectTableCardComment> _comments;
        public IReadOnlyCollection<ProjectTableCardComment> Comments => _comments.AsReadOnly();

        private const string _ticketCardColor = "#b82b18";

        //Constructors
        protected ProjectTableCard()
        {
            _comments = new List<ProjectTableCardComment>();
        }

        public ProjectTableCard(Action<object> applier) : base(applier)
        {
            _comments = new List<ProjectTableCardComment>();
        }

        //Entity abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.ProjectTableCardCreated e:
                    Id = new ProjectTableCardId(Guid.NewGuid());
                    Title = ProjectTableCardTitle.FromString(e.Title);
                    Content = ProjectTableCardContent.Empty;
                    Color = ProjectTableCardColor.Default;
                    CreatorId = new ProjectTableCardCreatorId(e.CurrentUserId);
                    StartingDate = ProjectTableCardStartingDate.Now;
                    Deadline = ProjectTableCardDeadline.Default;
                    Finished = ProjectTableCardFinished.Unfinished;
                    Priority = ProjectTableCardPriority.Default;
                    OrdinalNumber = e.OrdinalNumber;
                    break;

                case Events.ProjectTicketCreated e:
                    Id = new ProjectTableCardId(Guid.NewGuid());
                    Title = ProjectTableCardTitle.FromString(e.Title);
                    Content = ProjectTableCardContent.FromString(e.Content);
                    Color = ProjectTableCardColor.Set(_ticketCardColor);
                    CreatorId = ProjectTableCardCreatorId.NoUser;
                    StartingDate = ProjectTableCardStartingDate.Now;
                    Deadline = ProjectTableCardDeadline.Default;
                    Finished = ProjectTableCardFinished.Unfinished;
                    Priority = ProjectTableCardPriority.Default;
                    OrdinalNumber = 0;
                    break;

                case Events.ProjectTableCardUpdated e:
                    Title = ProjectTableCardTitle.FromString(e.Title);
                    Content = ProjectTableCardContent.FromString(e.Content);
                    Color = ProjectTableCardColor.Set(e.Color);
                    StartingDate = ProjectTableCardStartingDate.Set(e.StartingDate);
                    Deadline = ProjectTableCardDeadline.Set(e.Notify, e.EndingDate, e.AssignedUserId);
                    Priority = ProjectTableCardPriority.Set(e.Priority);
                    break;

                case Events.ProjectTableCardStatusChanged e:
                    Finished = Finished.Finished ? ProjectTableCardFinished.Unfinished : ProjectTableCardFinished.Set(e.CurrentUserId);
                    break;

                case Events.ProjectTableCardOrdinalNumberChanged e:
                    OrdinalNumber = e.OrdinalNumber;
                    break;

                case Events.ProjectTableCardCommentCreated e:
                    var commentToAdd = new ProjectTableCardComment(Apply);
                    ApplyToEntity(commentToAdd, e);

                    _comments.Add(commentToAdd);
                    break;

                case Events.ProjectTableCardCommentDeleted e:
                    var commentToDelete = _comments
                        .Single(c => c.Id.Value == e.CommentId && c.CreatorId.Value == e.CurrentUserId);

                    _comments.Remove(commentToDelete);
                    break;
            }
        }
    }
}
