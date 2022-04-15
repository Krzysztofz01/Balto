using Balto.Domain.Core.Events;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Notes.NoteTags
{
    public class NoteTag : Entity
    {
        public NoteTagId TagId { get; private set; }

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
