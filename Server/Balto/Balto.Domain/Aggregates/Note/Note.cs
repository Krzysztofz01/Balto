using Balto.Domain.Common;
using Balto.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Balto.Domain.Aggregates.Note
{
    public class Note : AggregateRoot<NoteId>
    {
        //Persistence
        public Guid NoteId { get; private set; }


        //Properties
        public NoteTitle Title { get; private set; }
        public NoteContent Content { get; private set; }
        public NoteOwnerId OwnerId { get; private set; }
        public bool Public { get; private set; }

        private readonly List<NoteContributor> _contributors;
        public IReadOnlyCollection<NoteContributor> Contributors => _contributors.AsReadOnly();


        //Constructors
        protected Note()
        {
            _contributors = new List<NoteContributor>();
        }

        protected Note(NoteOwnerId ownerId, NoteTitle title, NoteContent content)
        {
            _contributors = new List<NoteContributor>();

            Apply(new Events.NoteCreated
            {
                Id = Guid.NewGuid(),
                OwnerId = ownerId,
                Title = title,
                Content = content
            });
        }


        //Methods
        public void Update(string title, string content) =>
            Apply(new Events.NoteUpdated
            {
                Id = Id,
                Title = title,
                Content = content
            });

        public void Delete(Guid currentUserId) =>
            Apply(new Events.NoteDeleted
            {
                Id = Id,
                CurrentUserId = currentUserId
            });

        public void ChangePublication(Guid currentUserId) =>
            Apply(new Events.NotePublicationChanged
            {
                Id = Id,
                CurrentUserId = currentUserId
            });

        public void AddContributor(Guid contributorId, Guid currentUserId) =>
            Apply(new Events.NoteContributorAdded
            {
                Id = Id,
                ContributorId = contributorId,
                CurrentUserId = currentUserId
            });

        public void DeleteContributor(Guid contributorId, Guid currentUserId) =>
            Apply(new Events.NoteContributorDeleted
            {
                Id = Id,
                ContributorId = contributorId,
                CurrentUserId = currentUserId
            });

        public void Leave(Guid currentUserId) =>
            Apply(new Events.NoteLeave
            {
                Id = Id,
                CurrentUserId = currentUserId
            });

        private void ValidateAccess(Guid userId)
        {
            if (userId != OwnerId.Value)
                throw new UnauthorizedAccessException("Current user have no permission to perform this operation.");
        }


        //Aggregate root abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.NoteCreated e:
                    Id = new NoteId(e.Id);
                    OwnerId = new NoteOwnerId(e.OwnerId);
                    Title = NoteTitle.FromString(e.Title);
                    Content = NoteContent.FromString(e.Content);
                    break;

                case Events.NoteUpdated e:
                    Title = NoteTitle.FromString(e.Title);
                    Content = NoteContent.FromString(e.Content);
                    break;

                case Events.NoteDeleted e:
                    ValidateAccess(e.CurrentUserId);

                    SetAsDeleted();
                    break;

                case Events.NotePublicationChanged e:
                    ValidateAccess(e.CurrentUserId);

                    Public = !Public;
                    break;

                case Events.NoteContributorAdded e:
                    ValidateAccess(e.CurrentUserId);

                    var contributor = new NoteContributor(Apply);
                    ApplyToEntity(contributor, e);

                    _contributors.Add(contributor);
                    break;

                case Events.NoteContributorDeleted e:
                    ValidateAccess(e.CurrentUserId);

                    var targetUserForDelete = _contributors.Single(u => u.Id.Value == e.ContributorId);
                    _contributors.Remove(targetUserForDelete);
                    break;

                case Events.NoteLeave e:
                    if (e.CurrentUserId == OwnerId.Value)
                        throw new InvalidOperationException("The note owner can not leave the note.");

                    var targetUserForLeave = _contributors.Single(u => u.Id.Value == e.CurrentUserId);
                    _contributors.Remove(targetUserForLeave);
                    break;
            }
        }

        protected override void EnsureValidState()
        {
            //Null check
            bool valid = Id != null && OwnerId != null &&
                _contributors != null && Title != null && Content != null;

            if (!valid)
                throw new InvalidEntityStateException(this, "Final property validation failed.");
        }


        //Factory
        public static class Factory
        {
            public static Note Create(NoteOwnerId ownerId, NoteTitle title, NoteContent content)
            {
                return new Note(ownerId, title, content);
            }
        }
    }
}
