using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;
using static Balto.Domain.Projects.Events;

namespace Balto.Domain.Projects.ProjectTags
{
    public class ProjectTag : Entity
    {
        public ProjectTagId TagId { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            switch (@event)
            {
                case V1.ProjectTaskTagUnassigned e: When(e); break;

                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = TagId == null;

            if (isNull)
                throw new BusinessLogicException("The project tag entity properties can not be null.");
        }

        private void When(V1.ProjectTaskTagUnassigned _)
        {
            DeletedAt = DateTime.Now;
        }

        private ProjectTag() { }
        
        public static class Factory
        {
            public static ProjectTag Create(V1.ProjectTaskTagAssigned @event)
            {
                return new ProjectTag
                {
                    TagId = ProjectTagId.FromGuid(@event.TagId)
                };
            }
        }
    }
}
