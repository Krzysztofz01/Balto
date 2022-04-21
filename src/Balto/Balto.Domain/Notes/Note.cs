using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;
using Balto.Domain.Notes.NoteContributors;
using Balto.Domain.Notes.NoteSnapshots;
using Balto.Domain.Notes.NoteTags;
using System;
using System.Collections.Generic;
using System.Linq;
using static Balto.Domain.Notes.Events;

namespace Balto.Domain.Notes
{
    public class Note : AggregateRoot
    {
        public NoteTitle Title { get; private set; }
        public NoteContent Content { get; private set; }
        public NoteOwnerId OwnerId { get; private set; }

        private readonly List<NoteContributor> _contributors;
        public IReadOnlyCollection<NoteContributor> Contributors => _contributors.SkipDeleted().AsReadOnly();

        private readonly List<NoteTag> _tags;
        public IReadOnlyCollection<NoteTag> Tags => _tags.SkipDeleted().AsReadOnly();

        public readonly List<NoteSnapshot> _snapshots;
        public IReadOnlyCollection<NoteSnapshot> Snapshoots => _snapshots.SkipDeleted().AsReadOnly();

        protected override void Handle(IEventBase @event)
        {
            switch (@event)
            {
                case V1.NoteUpdated e: When(e); break;
                case V1.NoteDeleted e: When(e); break;

                case V1.NoteContributorAdded e: When(e); break;
                case V1.NoteContributorDeleted e: When(e); break;
                case V1.NoteContributorUpdated e: When(e); break;
                case V1.NoteContributorLeft e: When(e); break;

                case V1.NoteTagAssigned e: When(e); break;
                case V1.NoteTagUnassigned e: When(e); break;

                case V1.NoteSnapshotCreated e: When(e); break;
                case V1.NoteSnapshotDeleted e: When(e); break;

                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = Title == null || Content == null ||
                OwnerId == null || Tags == null || Contributors == null || Snapshoots == null;

            if (isNull)
                throw new BusinessLogicException("The note aggregate properties can not be null.");
        }

        private void When(V1.NoteUpdated @event)
        {
            CheckIfWritePermission(@event);

            Title = NoteTitle.FromString(@event.Title);
            Content = NoteContent.FromString(@event.Content);
        }

        private void When(V1.NoteDeleted @event)
        {
            CheckIfOwner(@event);

            DeletedAt = DateTime.Now;
        }

        private void When(V1.NoteContributorAdded @event)
        {
            CheckIfOwner(@event);

            if (@event.UserId == OwnerId)
                throw new InvalidOperationException("The owner is already a contributor.");

            var contributor = _contributors.SingleOrDefault(c => c.IdentityId.Value == @event.UserId);        
            if (contributor != null)
            {
                if (contributor.DeletedAt is null)
                    throw new InvalidOperationException("This identity is already a contributor.");

                if (contributor.DeletedAt is not null)
                    _contributors.Remove(contributor);
            }

            _contributors.Add(NoteContributor.Factory.Create(@event));
        }

        private void When(V1.NoteContributorDeleted @event)
        {
            CheckIfOwner(@event);

            var contributor = _contributors
                .SkipDeleted()
                .Single(c => c.IdentityId.Value == @event.UserId);

            contributor.Apply(@event);
        }

        private void When(V1.NoteContributorUpdated @event)
        {
            CheckIfOwner(@event);

            var contributor = _contributors
                .SkipDeleted()
                .Single(c => c.IdentityId.Value == @event.UserId);

            contributor.Apply(@event);
        }

        private void When(V1.NoteContributorLeft @event)
        {
            if (OwnerId == @event.CurrentUserId)
                throw new InvalidOperationException("The owner can not leave the project.");

            var contributor = _contributors
                .SkipDeleted()
                .Single(c => c.IdentityId.Value == @event.CurrentUserId);

            contributor.Apply(new V1.NoteContributorDeleted
            {
                CurrentUserId = @event.CurrentUserId,
                Id = @event.Id,
                UserId = @event.CurrentUserId
            });
        }

        private void When(V1.NoteTagAssigned @event)
        {
            CheckIfWritePermission(@event);

            if (_tags.SkipDeleted().Any(t => t.TagId == @event.TagId))
                throw new BusinessLogicException("This tag is already assigned to this note.");

            _tags.Add(NoteTag.Factory.Create(@event));
        }

        private void When(V1.NoteTagUnassigned @event)
        {
            CheckIfWritePermission(@event);

            var tag = _tags.SkipDeleted().Single(t => t.TagId.Value == @event.TagId);

            tag.Apply(@event);
        }

        private void When(V1.NoteSnapshotCreated @event)
        {
            CheckIfOwner(@event);

            if (@event.Content != Content.Value)
                throw new BusinessLogicException("The actual content is different from the requested snapshot content.");

            _snapshots.Add(NoteSnapshot.Factory.Create(@event));
        }

        private void When(V1.NoteSnapshotDeleted @event)
        {
            CheckIfOwner(@event);

            var snapshot = _snapshots.SkipDeleted().Single(t => t.Id == @event.SnapshotId);

            snapshot.Apply(@event);
        }

        private void CheckIfOwner(IAuthorizableEvent @event)
        {
            if (@event.CurrentUserId == OwnerId) return;

            throw new InvalidOperationException("No permission to perform this operation.");
        }

        private void CheckIfWritePermission(IAuthorizableEvent @event)
        {
            if (@event.CurrentUserId == OwnerId) return;

            if (_contributors.SkipDeleted().Any(c => c.IdentityId.Value == @event.CurrentUserId && c.AccessRole.Value == ContributorAccessRole.ReadWrite)) return;

            throw new InvalidOperationException("No permission to perform this operation.");
        }

        public Note()
        {
            _contributors = new List<NoteContributor>();
            _tags = new List<NoteTag>();
            _snapshots = new List<NoteSnapshot>();
        }

        public static class Factory
        {
            public static Note Create(V1.NoteCreated @event)
            {
                return new Note
                {
                    Title = NoteTitle.FromString(@event.Title),
                    Content = NoteContent.Empty,
                    OwnerId = NoteOwnerId.FromGuid(@event.CurrentUserId)
                };
            }
        }
    }
}
