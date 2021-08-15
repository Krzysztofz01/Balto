using System;

namespace Balto.Application.Aggregates.User
{
    public static class Contracts
    {
        public static class V1
        {
            public class UserRegister
            {
                public string Name { get; set; }
                public string Email { get; set; }
                public string Password { get; set; }
                public string PasswordRepate { get; set; }
            }

            public class UserLogin
            {
                public string Email { get; set; }
                public string Password { get; set; }
            }

            public class UserLogout
            {
                public string Token { get; set; }
            }

            public class UserRefresh
            {
                public string Token { get; set; }
            }

            public class UserResetPassword
            {
                public string Password { get; set; }
                public string PasswordRepeat { get; set; }
            }

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
