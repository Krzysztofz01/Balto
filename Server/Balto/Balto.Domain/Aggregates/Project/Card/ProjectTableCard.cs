using Balto.Domain.Common;
using System;
using System.Collections.Generic;

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

        private readonly List<ProjectTableCardComment> _comments;
        public IReadOnlyCollection<ProjectTableCardComment> Comments => _comments.AsReadOnly();


        //Constructors
        protected ProjectTableCard()
        {
            _comments = new List<ProjectTableCardComment>();
        }

        public ProjectTableCard(Action<object> applier) : base(applier)
        {
            _comments = new List<ProjectTableCardComment>();
        }


        //Methods



        //Entity abstraction implementation
        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
