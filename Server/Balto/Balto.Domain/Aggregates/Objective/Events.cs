using System;

namespace Balto.Domain.Aggregates.Objective
{
    public static class Events
    {
        public class ObjectiveCreated
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public ObjectivePriorityType Priority { get; set; }
            public bool Daily { get; set; }
            public DateTime StartingDate { get; set; }
            public DateTime EndingDate { get; set; }
        }

        public class ObjectiveInformationsChanged
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public ObjectivePriorityType Priority { get; set; }
        }

        public class ObjectiveFinishStateChanged
        {
            public Guid Id { get; set; }
        }

        public class ObjectiveDeleted
        {
            public Guid Id { get; set; }
        }
    }
}
