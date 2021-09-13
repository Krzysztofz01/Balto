using Balto.Domain.Aggregates.Project.Card;
using System;

namespace Balto.Domain.Aggregates.Project
{
    public static class Events
    {
        //Project aggregate root related events
        public class ProjectCreated
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class ProjectUpdated
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class ProjectDeleted
        {
            public Guid Id { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class ProjectContributorAdded
        {
            public Guid Id { get; set; }
            public Guid ContributorId { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class ProjectContributorDeleted
        {
            public Guid Id { get; set; }
            public Guid ContributorId { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class ProjectLeave
        {
            public Guid Id { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class ProjectTicketStatusChanged
        {
            public Guid Id { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class ProjectTicketCreated
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        //Project table entity related
        public class ProjectTableCreated
        {
            public Guid Id { get; set; }
            public Guid CurrentUserId { get; set; }
            public string Title { get; set; }
        }

        public class ProjectTableUpdated
        {
            public Guid Id { get; set; }
            public Guid TableId { get; set; }
            public Guid CurrentUserId { get; set; }
            public string Title { get; set; }
            public string Color { get; set; }
        }

        public class ProjectTableDeleted
        {
            public Guid Id { get; set; }
            public Guid TableId { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        //Project table card entity related

        public class ProjectTableCardCreated
        {
            public Guid Id { get; set; }
            public Guid TableId { get; set; }
            public Guid CurrentUserId { get; set; }
            public string Title { get; set; }
        }

        public class ProjectTableCardUpdated
        {
            public Guid Id { get; set; }
            public Guid CardId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public string Color { get; set; }
            public DateTime StartingDate { get; set; }
            public bool Notify { get; set; }
            public DateTime? EndingDate { get; set; }
            public Guid? AssignedUserId { get; set; }
            public CardPriorityType Priority { get; set; }
        }

        public class ProjectTableCardDeleted
        {
            public Guid Id { get; set; }
            public Guid CardId { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        public class ProjectTableCardStatusChanged
        {
            public Guid Id { get; set; }
            public Guid CardId { get; set; }
            public Guid CurrentUserId { get; set; }
        }

        //Project table card comment entity related

        public class ProjectTableCardCommentCreated
        {
            public Guid Id { get; set; }
            public Guid CardId { get; set; }
            public Guid CurrentUserId { get; set; }
            public string Content { get; set; }
        }

        public class ProjectTableCardCommentDeleted
        {
            public Guid Id { get; set; }
            public Guid CommentId { get; set; }
            public Guid CurrentUserId { get; set; }
        }
    }
}
