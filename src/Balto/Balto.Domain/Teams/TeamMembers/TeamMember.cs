using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;
using static Balto.Domain.Teams.Events;

namespace Balto.Domain.Teams.TeamMembers
{
    public class TeamMember : Entity
    {
        public TeamMemberIdentityId IdentityId { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            switch (@event)
            {
                case V1.TeamMemberDeleted e: When(e); break;

                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        private void When(V1.TeamMemberDeleted _)
        {
            DeletedAt = DateTime.Now;
        }

        protected override void Validate()
        {
            bool isNull = IdentityId is null;

            if (isNull)
                throw new BusinessLogicException("The project aggregate properties can not be null.");
        }

        private TeamMember() { }

        public static class Factory
        {
            public static TeamMember Create(V1.TeamMemberAdded @event)
            {
                return new TeamMember
                {
                    IdentityId = TeamMemberIdentityId.FromGuid(@event.IdentityId)
                };
            }
        }
    }
}
