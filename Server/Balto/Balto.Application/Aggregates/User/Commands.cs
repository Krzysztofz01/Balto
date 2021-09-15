using System;
using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Aggregates.User
{
    public static class Commands
    {
        public static class V1
        {
            public class UserDelete
            {
                [Required]
                public Guid TargetUserId { get; set; }
            }

            public class UserActivate
            {
                [Required]
                public Guid TargetUserId { get; set; }
            }

            public class UserTeamUpdate
            {
                [Required]
                public Guid TargetUserId { get; set; }

                [Required]
                public Guid TeamId { get; set; }
            }

            public class UserColorUpdate
            {
                [Required]
                public Guid TargetUserId { get; set; }

                [Required]
                public string Color { get; set; }
            }

            public class UserLeaderStatusUpdate
            {
                [Required]
                public Guid TargetUserId { get; set; }
            }
        }
    }
}
