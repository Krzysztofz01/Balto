using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using Balto.Domain.Identities.Tokens;
using System;
using System.Collections.Generic;
using static Balto.Domain.Identities.Events;

namespace Balto.Domain.Identities
{
    public class Identity : AggregateRoot
    {
        public IdentityName Name { get; private set; }
        public IdentityEmail Email { get; private set; }
        public IdentityPasswordHash PasswordHash { get; private set; }
        public IdentityLastLogin LastLogin { get; private set; }
        public IdentityRole Role { get; private set; }
        public IdentityActivation Activation { get; private set; }
        public IdentityColor Color { get; private set; }
        public IdentityTeamId TeamId { get; private set; }

        private readonly List<Token> _tokens;
        public IReadOnlyCollection<Token> Tokens => _tokens.AsReadOnly();

        protected override void Handle(IEventBase @event)
        {
            switch(@event)
            {
                case V1.IdentityUpdated e: When(e); break;
                case V1.IdentityDeleted e: When(e); break;
                case V1.IdentityActivationChanged e: When(e); break;
                case V1.IdentityRoleChanged e: When(e); break;
                case V1.IdentityTeamChanged e: When(e); break;
                case V1.IdentityAuthenticated e: When(e); break;
                case V1.IdentityPasswordChanged e: When(e); break;
                case V1.IdentityTokenRefreshed e: When(e); break;
                case V1.IdentityTokenRevoked e: When(e); break;
                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = Name == null || Email == null || PasswordHash == null ||
                LastLogin == null || Role == null || Activation == null || Color == null || Tokens == null;

            if (isNull)
                throw new BusinessLogicException("The identity aggregate properties can not be null.");
        }

        private void When(V1.IdentityDeleted _)
        {
            DeletedAt = DateTime.Now;

            _tokens.Clear();
        }

        private void When(V1.IdentityUpdated @event)
        {
            Color = IdentityColor.FromString(@event.Color);
        }

        private void When(V1.IdentityActivationChanged @event)
        {
            Activation = @event.Activated ?
                IdentityActivation.Active : IdentityActivation.Inactive;
        }

        private void When(V1.IdentityRoleChanged @event)
        {
            Role = IdentityRole.FromUserRole(@event.Role);
        }

        private void When(V1.IdentityTeamChanged @event)
        {
            TeamId = @event.TeamId.HasValue ?
                IdentityTeamId.FromGuid(@event.TeamId.Value) : IdentityTeamId.NoTeam;
        }

        private void When(V1.IdentityAuthenticated @event)
        {
            if (!Activation)
                throw new InvalidOperationException("Given identity is not active.");

            LastLogin = IdentityLastLogin.FromString(@event.IpAddress);

            _tokens.Add(Token.Factory.Create(new V1.TokenCreated
            {
                TokenHash = @event.TokenHash,
                IpAddress = @event.IpAddress
            }));
        }

        private void When(V1.IdentityPasswordChanged @event)
        {
            PasswordHash = IdentityPasswordHash.FromString(@event.PasswordHash);

            //TODO: Revoke all active tokens
            throw new NotImplementedException();
        }

        private void When(V1.IdentityTokenRefreshed @event)
        {
            throw new NotImplementedException();
        }

        private void When(V1.IdentityTokenRevoked @event)
        {
            throw new NotImplementedException();
        }

        private Identity()
        {
            _tokens = new List<Token>();
        }

        public static class Factory
        {
            public static Identity Create(V1.IdentityCreated @event)
            {
                return new Identity
                {
                    Name = IdentityName.FromString(@event.Name),
                    Email = IdentityEmail.FromString(@event.Email),
                    PasswordHash = IdentityPasswordHash.FromString(@event.PasswordHash),
                    LastLogin = IdentityLastLogin.FromString(@event.IpAddress),
                    Role = IdentityRole.Default,
                    Activation = IdentityActivation.Inactive,
                    Color = IdentityColor.Default,
                    TeamId = IdentityTeamId.NoTeam
                };
            }
        }
    }
}
