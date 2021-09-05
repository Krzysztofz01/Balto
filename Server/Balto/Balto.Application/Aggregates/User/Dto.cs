using System;

namespace Balto.Application.Aggregates.User
{
    public static class Dto
    {
        public static class V1
        {
            public class UserDetails
            {
                public Guid Id { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }
                public Guid TeamId { get; set; }
                public string Color { get; set; }
                public string LastLoginIp { get; set; }
                public string LastLoginDate { get; set; }
                public bool IsLeader { get; set; }
                public bool IsActivated { get; set; }
            }

            public class UserSimple
            {
                public Guid Id { get; set; }
                public string Name { get; set; }
                public string Email { get; set; }
                public Guid TeamId { get; set; }
                public string Color { get; set; }
            }
        }
    }
}
