using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardComment : Entity<ProjectTableCardCommentId>
    {
        //Persistence
        public Guid ProjectTableCardCommentId { get; private set; }

        //Properties
        public ProjectTableCardCommentContent Content { get; private set; }
        public ProjectTableCardCommentCreatorId CreatorId { get; private set; }


        //Constructors
        protected ProjectTableCardComment() { }
        public ProjectTableCardComment(Action<object> applier) : base(applier) { }


        //Entity abstraction implementation
        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
