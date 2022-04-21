using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;
using static Balto.Domain.Notes.Events;

namespace Balto.Domain.Notes.NoteContributors
{
    public class NoteContributor : Entity
    {
        public NoteContributorIdentityId IdentityId { get; private set; }
        public NoteContributorAccessRole AccessRole { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            switch (@event)
            {
                case V1.NoteContributorUpdated e: When(e); break;
                case V1.NoteContributorDeleted e: When(e); break;

                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = IdentityId == null || AccessRole == null;

            if (isNull)
                throw new BusinessLogicException("The note contributor entity properties can not be null.");
        }

        private void When(V1.NoteContributorUpdated @event)
        {
            AccessRole = NoteContributorAccessRole.FromContributorAccessRole(@event.Role);
        }

        private void When(V1.NoteContributorDeleted @event)
        {
            DeletedAt = DateTime.Now;
        }

        private NoteContributor() { }

        public static class Factory
        {
            public static NoteContributor Create(V1.NoteContributorAdded @event)
            {
                return new NoteContributor
                {
                    IdentityId = NoteContributorIdentityId.FromGuid(@event.UserId),
                    AccessRole = NoteContributorAccessRole.Default
                };
            }
        }
    }
}
