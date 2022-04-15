using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;
using static Balto.Domain.Tags.Events;

namespace Balto.Domain.Tags
{
    public class Tag : AggregateRoot
    {
        public TagTitle Title { get; private set; }
        public TagColor Color { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            switch (@event)
            {
                case V1.TagDeleted e: When(e); break;

                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = Title == null || Color == null;

            if (isNull)
                throw new BusinessLogicException("The tag aggregate properties can not be null.");
        }


        private void When(V1.TagDeleted @event)
        {
            DeletedAt = DateTime.Now;
        }

        private Tag() { }

        public static class Factory
        {
            public static Tag Create(V1.TagCreated @event)
            {
                return new Tag
                {
                    Title = TagTitle.FromString(@event.Title),
                    Color = TagColor.FromString(@event.Color)
                };
            }
        }
    }
}
