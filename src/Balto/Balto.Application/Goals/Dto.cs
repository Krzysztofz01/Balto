using Balto.Domain.Shared;
using System;

namespace Balto.Application.Goals
{
    public static class Dto
    {
        public class GoalSimple
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public PriorityTypes Priority { get; set; }
            public string Color { get; set; }
            public DateTime? Deadline { get; set; }
            public bool IsRecurring { get; set; }
            public bool Finished { get; set; }
        }

        public class GoalDetails
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public PriorityTypes Priority { get; set; }
            public string Color { get; set; }
            public DateTime StartingDate { get; set; }
            public DateTime? Deadline { get; set; }
            public bool IsRecurring { get; set; }
            public bool Finished { get; set; }
            public DateTime? FinishDate { get; set; }
        }
    }
}
