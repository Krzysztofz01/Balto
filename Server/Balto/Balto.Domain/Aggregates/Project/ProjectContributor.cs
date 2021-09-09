using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project
{
    public class ProjectContributor : Entity<ProjectContributorId>
    {
        //Presistance
        public Guid ProjectContributorId { get; private set; }

        //Properties


        //Constructors
        protected ProjectContributor() { }
        public ProjectContributor(Action<object> applier) : base(applier) { }


        //Methods


        //Entity abstraction implementation
        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.ProjectContributorAdded e:
                    Id = new ProjectContributorId(e.ContributorId);
                    break;
            }
        }
    }
}
