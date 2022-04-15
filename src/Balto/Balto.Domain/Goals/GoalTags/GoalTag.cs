using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;
using static Balto.Domain.Goals.Events;

namespace Balto.Domain.Goals.GoalTags
{
    public class GoalTag : Entity
    {
        public GoalTagId TagId { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            switch (@event)
            {
                case V1.GoalTagUnassigned e: When(e); break;

                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = TagId == null;

            if (isNull)
                throw new BusinessLogicException("The goal tag entity properties can not be null.");
        }

        private void When(V1.GoalTagUnassigned _)
        {
            DeletedAt = DateTime.Now;
        }

        private GoalTag() { }
    
        public static class Factory
        {
            public static GoalTag Create(V1.GoalTagAssigned @event)
            {
                return new GoalTag
                {
                    TagId = GoalTagId.FromGuid(@event.TagId)
                };
            }
        }
    }
}
