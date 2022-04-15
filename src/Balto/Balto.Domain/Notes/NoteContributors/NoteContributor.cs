using Balto.Domain.Core.Events;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Notes.NoteContributors
{
    public class NoteContributor : Entity
    {
        public NoteContributorIdentityId IdentityId { get; private set; }
        public NoteContributorAccessRole AccessRole { get; private set; }

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
