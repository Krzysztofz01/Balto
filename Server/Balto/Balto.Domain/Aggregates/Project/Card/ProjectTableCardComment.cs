using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardComment : Entity<ProjectTableCardCommentId>
    {
        //Persistence
        public Guid CommentId { get; private set; }

        //Properties
        public ProjectTableCardCommentContent Content { get; private set; }
        public ProjectTableCardCommentCreatorId CreatorId { get; private set; }
        public ProjectTableCardCommentCreateDate CreateDate { get; private set; }


        //Constructors
        protected ProjectTableCardComment() { }
        public ProjectTableCardComment(Action<object> applier) : base(applier) { }


        //Entity abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.ProjectTableCardCommentCreated e:
                    Id = new ProjectTableCardCommentId(Guid.NewGuid());
                    Content = ProjectTableCardCommentContent.FromString(e.Content);
                    CreatorId = new ProjectTableCardCommentCreatorId(e.CurrentUserId);
                    CreateDate = ProjectTableCardCommentCreateDate.Now;
                    break;
            }
        }
    }
}
