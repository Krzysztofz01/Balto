using Balto.Domain.Core.Events;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Projects.ProjectContributors
{
    public class ProjectContributor : Entity
    {
        public ProjectContributorIdentityId IdentityId { get; private init; }
        public ProjectContributorRole Role { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        private ProjectContributor() { }

        public static class Factory
        {
            public static ProjectContributor Create()
            {
                throw new NotImplementedException();
            }
        }
    }
}
