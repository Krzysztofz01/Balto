using Balto.Domain.Core.Events;
using Balto.Domain.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balto.Domain.Projects.ProjectContributors
{
    public class ProjectContributor : Entity
    {
        public ProjectContributorIdentityId IdentityId { get; private init; }

        protected override void Handle(IEventBase @event)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
