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

        //TODO Ticket

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
    }
}
