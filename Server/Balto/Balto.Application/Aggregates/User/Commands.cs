using System;

namespace Balto.Application.Aggregates.User
{
    public static class Commands
    {
        public static class V1
        {
            public class UserDelete
            {
                public Guid TargetUserId { get; set; }
            }

            public class UserActivate
            {
                public Guid TargetUserId { get; set; }
            }

            public class UserTeamUpdate
            {
                public Guid TargetUserId { get; set; }
                public Guid TeamId { get; set; }
            }

            public class UserColorUpdate
            {
                public Guid TargetUserId { get; set; }
                public string Color { get; set; }
            }

            public class UserLeaderStatusUpdate
            {
                public Guid TargetUserId { get; set; }
            }
        }
    }
}
