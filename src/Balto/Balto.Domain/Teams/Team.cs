using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;
using Balto.Domain.Teams.TeamMembers;
using System;
using System.Collections.Generic;
using System.Linq;
using static Balto.Domain.Teams.Events;

namespace Balto.Domain.Teams
{
    public class Team : AggregateRoot
    {
        public TeamName Name { get; private set; }
        public TeamColor Color { get; private set; }

        private readonly List<TeamMember> _members;
        public IReadOnlyCollection<TeamMember> Members => _members.SkipDeleted().AsReadOnly();

        protected override void Handle(IEventBase @event)
        {
            switch (@event)
            {
                case V1.TeamDeleted e: When(e); break;
                case V1.TeamMemberAdded e: When(e); break;
                case V1.TeamMemberDeleted e: When(e); break;

                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        private void When(V1.TeamMemberDeleted @event)
        {
            var member = _members
                .SkipDeleted()
                .Single(m => m.IdentityId.Value == @event.IdentityId);

            member.Apply(@event);
        }

        private void When(V1.TeamMemberAdded @event)
        {
            var member = _members.SingleOrDefault(m => m.IdentityId.Value == @event.IdentityId);
            if (member is not null)
            {
                if (member.DeletedAt is null)
                    throw new BusinessLogicException("This identity is already a member of this team.");

                if (member.DeletedAt is not null)
                    _members.Remove(member);
            }

            _members.Add(TeamMember.Factory.Create(@event));
        }

        private void When(V1.TeamDeleted _)
        {
            DeletedAt = DateTime.Now;
        }

        protected override void Validate()
        {
            bool isNull = Name is null || Color is null || Members is null;

            if (isNull)
                throw new BusinessLogicException("The project aggregate properties can not be null.");
        }

        private Team()
        {
            _members = new List<TeamMember>();
        }

        public static class Factory
        {
            public static Team Create(V1.TeamCreated @event)
            {
                return new Team
                {
                    Name = TeamName.FromString(@event.Name),

                    Color = (@event.Color is not null)
                        ? TeamColor.FromString(@event.Color)
                        : TeamColor.Default
                };
            }
        }
    }
}
