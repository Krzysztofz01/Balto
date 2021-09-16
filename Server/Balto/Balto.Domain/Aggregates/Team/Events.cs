using System;

namespace Balto.Domain.Aggregates.Team
{
    public static class Events
    {
        public class TeamCreated
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
        }

        public class TeamDeleted
        {
            public Guid Id { get; set; }
        }

        public class TeamUpdated
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Color { get; set; }
        }
    }
}
