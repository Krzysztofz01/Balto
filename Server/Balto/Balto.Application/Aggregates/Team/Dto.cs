using System;

namespace Balto.Application.Aggregates.Team
{
    public static class Dto
    {
        public static class V1
        {
            public class TeamDetails
            {
                public Guid Id { get; set; }
                public string Name { get; set; }
                public string Color { get; set; }
            }

            public class TeamSimple
            {
                public Guid Id { get; set; }
                public string Name { get; set; }
                public string Color { get; set; }
            }
        }
    }
}
