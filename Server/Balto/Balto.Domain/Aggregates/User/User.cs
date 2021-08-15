using Balto.Domain.Common;
using Balto.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Balto.Domain.Aggregates.User
{
    public class User : AggregateRoot<UserId>
    {
        //Presistance
        public Guid UserId { get; private set; }

        //Properties
        public UserName Name { get; private set; }
        public UserEmail Email { get; private set; }
        public UserPassword Password { get; private set; }
        public UserTeamId TeamId { get; private set; }
        public UserColor Color { get; private set; }
        public UserLastLogin LastLogin { get; private set; }
        public bool IsLeader { get; private set; }
        public bool IsActivated { get; private set; }
        
        private readonly List<RefreshToken> _refreshTokens;
        public IReadOnlyCollection<RefreshToken> RefreshTokens => _refreshTokens.AsReadOnly();


        //Constructors
        protected User() 
        { 
            _refreshTokens = new List<RefreshToken>(); 
        }

        protected User(UserName name, UserEmail email, UserPassword password)
        {
            _refreshTokens = new List<RefreshToken>();
            
            Apply(new Events.UserCreated
            {
                Name = name,
                Email = email,
                Password = password
            });
        }          


        //Methods
        public void Authenticate(string ipAddress) =>
            Apply(new Events.UserAuthenticated
            {
                UserId = Id,
                IpAddress = ipAddress
            });

        public void TokenRefresh(string ipAddress, string token) =>
            Apply(new Events.UserTokenRefreshed
            {
                UserId = Id,
                IpAddress = ipAddress,
                Token = token
            });

        public void TokenRevoke(string token, string ipAddress) =>
            Apply(new Events.UserTokenRevoked
            {
                UserId = Id,
                IpAddress = ipAddress,
                Token = token
            });

        public void Activate() =>
            Apply(new Events.UserActivationChanged
            {
                UserId = Id
            });

        public void Delete() =>
            Apply(new Events.UserDeleted
            {
                UserId = Id
            });

        public void ChangePassword(string password) =>
            Apply(new Events.UserPasswordChanged
            {
                UserId = Id,
                Password = password
            });

        public void PromoteToLeader() =>
            Apply(new Events.UserLeaderStatusChanged
            {
                UserId = Id
            });

        public void SetTeam(Guid teamId) =>
            Apply(new Events.UserTeamChanged
            {
                UserId = Id,
                TeamId = teamId
            });

        public void SetColor(string color) =>
            Apply(new Events.UserColorChanged
            {
                UserId = Id,
                Color = color
            });

        //Aggregate root abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.UserCreated e:
                    Id = new UserId(e.Id);
                    Name = UserName.FromString(e.Name);
                    Email = UserEmail.FromString(e.Email);
                    Password = UserPassword.FromHash(e.Password);
                    TeamId = UserTeamId.NoTeam;
                    Color = UserColor.Default;
                    LastLogin = UserLastLogin.Set(e.IpAddress);
                    IsLeader = false;
                    IsActivated = false;
                    break;

                case Events.UserDeleted _:
                    SetAsDeleted();
                    break;

                case Events.UserAuthenticated e:
                    CheckActivationDuringAuthentication();

                    LastLogin = UserLastLogin.Set(e.IpAddress);

                    RefreshToken.Factory.Create(e.IpAddress);
                    break;

                case Events.UserActivationChanged _:
                    IsActivated = !IsActivated;
                    break;

                case Events.UserTokenRefreshed e:
                    CheckActivationDuringAuthentication();

                    var token = RefreshToken.Factory.Create(e.IpAddress);

                    _refreshTokens.Single(t => t.Token == e.Token)
                        .Revoke(e.IpAddress, token.Token);

                    _refreshTokens.Add(token);
                    break;

                case Events.UserTokenRevoked e:
                    _refreshTokens.Single(t => t.Token == e.Token)
                        .Revoke(e.IpAddress);
                    break;

                case Events.UserPasswordChanged e:
                    CheckActivationDuringAuthentication();

                    Password = UserPassword.FromHash(e.Password);
                    break;

                case Events.UserTeamChanged e:
                    TeamId = new UserTeamId(e.TeamId);
                    break;

                case Events.UserColorChanged e:
                    Color = UserColor.Set(e.Color);
                    break;

                case Events.UserLeaderStatusChanged _:
                    IsLeader = !IsLeader;
                    break;
            }
        }

        private void CheckActivationDuringAuthentication()
        {
            if (!IsActivated) throw new InvalidEntityStateException(this, "Cannot authenticate if user account is not activated.");
        }

        protected override void EnsureValidState()
        {
            //Null check
            bool valid = Id != null && Name != null && Email != null &&
                Password != null && Color != null && LastLogin != null;

            if (!valid)
                throw new InvalidEntityStateException(this, "Final property validation failed.");
        }

        //Factory
        public static class Factory
        {
            public static User Create(UserName name, UserEmail email, UserPassword password)
            {
                return new User(name, email, password);
            }
        }

    }
}
