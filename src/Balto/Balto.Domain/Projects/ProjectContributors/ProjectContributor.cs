using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;
using static Balto.Domain.Projects.Events;

namespace Balto.Domain.Projects.ProjectContributors
{
    public class ProjectContributor : Entity
    {
        public ProjectContributorIdentityId IdentityId { get; private init; }
        public ProjectContributorRole Role { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            switch(@event)
            {
                case V1.ProjectContributorUpdated e: When(e); break;
                case V1.ProjectContributorDeleted e: When(e); break;
                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = IdentityId == null || Role == null;

            if (isNull)
                throw new BusinessLogicException("The project contributor entity properties can not be null.");
        }

        private void When(V1.ProjectContributorUpdated @event)
        {
            Role = ProjectContributorRole.FromContributorRole(@event.Role);
        }

        private void When(V1.ProjectContributorDeleted _)
        {
            DeletedAt = DateTime.Now;
        }

        private ProjectContributor() { }

        public static class Factory
        {
            public static ProjectContributor Create(V1.ProjectContributorAdded @event)
            {
                return new ProjectContributor
                {
                    IdentityId = ProjectContributorIdentityId.FromGuid(@event.UserId),
                    Role = ProjectContributorRole.Default
                };
            }
        }
    }
}
