using Balto.Domain.Core.Events;
using Balto.Domain.Projects.ProjectContributors;
using Balto.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Balto.Domain.Projects
{
    public static class Events
    {
        public static class V1
        {
            //Project aggreagate related events
            public class ProjectCreated : IAuthorizableEvent
            {
                public Guid CurrentUserId { get; set; }
                public string Title { get; set; }
            }

            public class ProjectUpdated : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public string Title { get; set; }
                public bool? TicketStatus { get; set; }
            }

            public class ProjectDeleted : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
            }

            public class TicketPushed : IEvent
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
                public string Content { get; set; }
            }
            
            //Project contributor entity related events
            public class ProjectContributorAdded : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public Guid UserId { get; set; }
            }
            
            public class ProjectContributorDeleted : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public Guid UserId { get; set; }
            }

            public class ProjectContributorUpdated : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public Guid UserId { get; set; }
                public ContributorRole Role { get; set; }
            }

            public class ProjectContributorLeft : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
            }

            //Project table entity related events
            public class ProjectTableCreated : IEvent
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
            }

            public class ProjectTableUpdated : IEvent
            {
                public Guid Id { get; set; }
                public Guid TableId { get; set; }
                public string Title { get; set; }
                public string Color { get; set; }
            }

            public class ProjectTableDeleted : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid TableId { get; set; }
                public Guid CurrentUserId { get; set; }
            }

            //Project task entity related events
            public class ProjectTaskCreated : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public Guid TableId { get; set; }
                public string Title { get; set; }
            }

            public class ProjectTaskUpdated : IEvent
            {
                public Guid Id { get; set; }
                public Guid TableId { get; set; }
                public Guid TaskId { get; set; }
                public string Title { get; set; }
                public string Content { get; set; }
                public Guid? AssignedContributorId { get; set; }
                public DateTime StartingDate { get; set; }
                public DateTime? Deadline { get; set; }
                public PriorityTypes Priority { get; set; }
            }

            public class ProjectTaskDeleted : IEvent
            {
                public Guid Id { get; set; }
                public Guid TableId { get; set; }
                public Guid TaskId { get; set; }
            }

            public class ProjectTaskStatusChanged : IEvent, IAuthorizableEvent
            {
                public Guid Id { get; set; }
                public Guid CurrentUserId { get; set; }
                public Guid TableId { get; set; }
                public Guid TaskId { get; set; }
                public bool Status { get; set; }
            }

            public class ProjectTableTasksOrdinalNumbersChanged : IEvent
            {
                public Guid Id { get; set; }
                public Guid TableId { get; set; }
                public IEnumerable<Tuple<Guid, int>> IdOrdinalNumberPairs { get; set; }
            }

            public class ProjectTaskOrdinalNumbeChanged : IEvent
            {
                public Guid Id { get; set; }
                public Guid TableId { get; set; }
                public Guid TaskId { get; set; }
                public int OrdinalNumber { get; set; }
            }
        }
    }
}
