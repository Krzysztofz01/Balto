using System;

namespace Balto.Domain.Aggregates.User
{
    public class Events
    {
        public class UserCreated
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
            public string IpAddress { get; set; }
        }

        public class UserDeleted
        {
            public Guid UserId { get; set; }
        }

        public class UserAuthenticated
        {
            public Guid UserId { get; set; }
            public string IpAddress { get; set; }
        }

        public class UserActivationChanged
        {
            public Guid UserId { get; set; }
        }

        public class UserTokenRefreshed
        {
            public Guid UserId { get; set; }
            public string Token { get; set; }
            public string IpAddress { get; set; }
        }

        public class UserTokenRevoked
        {
            public Guid UserId { get; set; }
            public string Token { get; set; }
            public string IpAddress { get; set; }
        }

        public class UserPasswordChanged
        {
            public Guid UserId { get; set; }
            public string Password { get; set; }
        }

        public class UserTeamChanged
        {
            public Guid UserId { get; set; }
            public Guid TeamId { get; set; }
        }

        public class UserColorChanged
        {
            public Guid UserId { get; set; }
            public string Color { get; set; }
        }

        public class UserLeaderStatusChanged
        {
            public Guid UserId { get; set; }
        }

        public class RefreshTokenReplacedByTokenChanged
        {
            public Guid UserId { get; set; }
            public string Token { get; set; }
        }
    }
}
