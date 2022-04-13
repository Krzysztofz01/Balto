using Balto.Domain.Core.Events;
using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Goals
{
    public static class Events
    {
        public static class V1
        {
            public class GoalCreated : IAuthorizableEvent
            {
                public Guid CurrentUserId { get; set; }
                public string Title { get; set; }
            }

            public class GoalUpdated : IEvent
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
                public string Description { get; set; }
                public PriorityTypes Priority { get; set; }
                public string Color { get; set; }
                public DateTime StartingDate { get; set; }
                public DateTime? Deadline { get; set; }
                public bool IsRecurring { get; set; }
            }

            public class GoalDeleted : IEvent
            {
                public Guid Id { get; set; }
            }

            public class GoalStateChanged : IEvent
            {
                public Guid Id { get; set; }
                public bool State { get; set; }
            }

            public class GoalRecurringReset : IEvent
            {
                public Guid Id { get; set; }
            }

            public class GoalTagAssigned : IEvent
            {
                public Guid Id { get; set; }
                public Guid TagId { get; set; }
            }

            public class GoalTagUnassigned : IEvent
            {
                public Guid Id { get; set; }
                public Guid TagId { get; set; }
            }
        }
    }
}
