using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;
using static Balto.Domain.Notes.Events;

namespace Balto.Domain.Notes.NoteTags
{
    public class NoteTag : Entity
    {
        public NoteTagId TagId { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            switch (@event)
            {
                case V1.NoteTagUnassigned e: When(e); break;

                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = TagId == null;

            if (isNull)
                throw new BusinessLogicException("The note tag entity properties can not be null.");
        }

        private void When(V1.NoteTagUnassigned _)
        {
            DeletedAt = DateTime.Now;
        }

        private NoteTag() { }

        public static class Factory
        {
            public static NoteTag Create(V1.NoteTagAssigned @event)
            {
                return new NoteTag
                {
                    TagId = NoteTagId.FromGuid(@event.TagId)
                };
            }
        }
    }
}
