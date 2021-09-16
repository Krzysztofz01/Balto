using System;

namespace Balto.Application.Aggregates.Team
{
    public static class Commands
    {
        public static class V1
        {
            public class TeamCreate
            {
                public string Name { get; set; }
                public string Color { get; set; }
            }

            public class TeamUpdate
            {
                public Guid Id { get; set; }
                public string Name { get; set; }
                public string Color { get; set; }
            }

            public class TeamDelete
            {
                public Guid Id { get; set; }
            }
        }
    }
}
