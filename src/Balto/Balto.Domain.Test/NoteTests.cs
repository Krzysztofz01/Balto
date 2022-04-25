using Balto.Domain.Core.Exceptions;
using Balto.Domain.Notes;
using System;
using System.Linq;
using Xunit;

namespace Balto.Domain.Test
{
    public class NoteTests
    {
        [Fact]
        public void NoteShouldCreate()
        {
            Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = Guid.NewGuid()
            });
        }

        [Fact]
        public void NoteShouldUpdate()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            string expectedTitle = "Updated title";
            string expectedContent = "Updated content";

            note.Apply(new Events.V1.NoteUpdated
            {
                Title = expectedTitle,
                Content = expectedContent,
                CurrentUserId = ownerId,
                Id = note.Id
            });

            Assert.Equal(expectedTitle, note.Title);
            Assert.Equal(expectedContent, note.Content);
        }

        [Fact]
        public void NoteShouldThrowOnUpdateWithoutPermission()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            string expectedTitle = "Updated title";
            string expectedContent = "Updated content";

            Assert.Throws<BusinessLogicException>(() =>
            {
                note.Apply(new Events.V1.NoteUpdated
                {
                    Title = expectedTitle,
                    Content = expectedContent,
                    CurrentUserId = Guid.NewGuid(),
                    Id = note.Id
                });
            });
        }

        [Fact]
        public void NoteShouldDelete()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            note.Apply(new Events.V1.NoteDeleted
            {
                Id = note.Id,
                CurrentUserId = ownerId
            });

            DateTime? expected = DateTime.Now;

            Assert.Equal(expected.Value.Date, note.DeletedAt.Value.Date);
        }

        [Fact]
        public void NoteShouldThrowOnDeleteWithoutPermission()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Assert.Throws<BusinessLogicException>(() =>
            {
                note.Apply(new Events.V1.NoteDeleted
                {
                    Id = note.Id,
                    CurrentUserId = Guid.NewGuid()
                });
            });
        }

        [Fact]
        public void ProjectContributorShouldBeAdded()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            note.Apply(new Events.V1.NoteContributorAdded
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            var expected = conId;

            var actual = note.Contributors.Single().IdentityId.Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProjectContributorShouldThrowOnAddWithoutPermission()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Assert.Throws<BusinessLogicException>(() => note.Apply(new Events.V1.NoteContributorAdded
            {
                Id = note.Id,
                CurrentUserId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            }));
        }

        [Fact]
        public void ProjectContributorShouldThrowOnAddingCreator()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Assert.Throws<InvalidOperationException>(() => note.Apply(new Events.V1.NoteContributorAdded
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                UserId = ownerId
            }));
        }

        [Fact]
        public void ProjectContributorShouldThrowOnAddingKnownContributor()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            note.Apply(new Events.V1.NoteContributorAdded
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            Assert.Throws<InvalidOperationException>(() => note.Apply(new Events.V1.NoteContributorAdded
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                UserId = conId
            }));
        }

        [Fact]
        public void ProjectContributorShouldBeDeleted()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            note.Apply(new Events.V1.NoteContributorAdded
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            int exptectedCount = 1;

            Assert.Equal(exptectedCount, note.Contributors.Count);

            note.Apply(new Events.V1.NoteContributorDeleted
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            int exptectedCountAfter = 0;

            Assert.Equal(exptectedCountAfter, note.Contributors.Count);
        }

        [Fact]
        public void ProjectContributorDeleteShouldThrowWithoutPermission()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            note.Apply(new Events.V1.NoteContributorAdded
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            int exptectedCount = 1;

            Assert.Equal(exptectedCount, note.Contributors.Count);

            Assert.Throws<BusinessLogicException>(() => note.Apply(new Events.V1.NoteContributorDeleted
            {
                Id = note.Id,
                CurrentUserId = Guid.NewGuid(),
                UserId = conId
            }));
        }

        [Fact]
        public void ProjectContributorShouldLeftWhenContributor()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            note.Apply(new Events.V1.NoteContributorAdded
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            int exptectedCount = 1;

            Assert.Equal(exptectedCount, note.Contributors.Count);

            note.Apply(new Events.V1.NoteContributorLeft
            {
                Id = note.Id,
                CurrentUserId = conId
            });

            int exptectedCountAfter = 0;

            Assert.Equal(exptectedCountAfter, note.Contributors.Count);
        }

        [Fact]
        public void ProjectContributorLeftShouldThrowWhenOwner()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Assert.Throws<InvalidOperationException>(() => note.Apply(new Events.V1.NoteContributorLeft
            {
                Id = note.Id,
                CurrentUserId = ownerId
            }));
        }

        [Fact]
        public void ProjectReadonlyContributorShouldNotUpdate()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            note.Apply(new Events.V1.NoteContributorAdded
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            note.Apply(new Events.V1.NoteContributorUpdated
            {
                Id = note.Id,
                UserId = conId,
                CurrentUserId = ownerId,
                Role = Notes.NoteContributors.ContributorAccessRole.ReadOnly
            });

            Assert.Throws<BusinessLogicException>(() =>
            {
                note.Apply(new Events.V1.NoteUpdated
                {
                    Title = string.Empty,
                    Content = string.Empty,
                    CurrentUserId = conId,
                    Id = note.Id
                });
            });
        }

        [Fact]
        public void ProjectReadWriteContributorShouldUpdate()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            note.Apply(new Events.V1.NoteContributorAdded
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            note.Apply(new Events.V1.NoteContributorUpdated
            {
                Id = note.Id,
                UserId = conId,
                CurrentUserId = ownerId,
                Role = Notes.NoteContributors.ContributorAccessRole.ReadWrite
            });

            string expectedTitle = "Updated title";
            string expectedContent = "Updated content";

            note.Apply(new Events.V1.NoteUpdated
            {
                Title = expectedTitle,
                Content = expectedContent,
                CurrentUserId = conId,
                Id = note.Id
            });

            Assert.Equal(expectedTitle, note.Title);
            Assert.Equal(expectedContent, note.Content);
        }

        [Fact]
        public void NoteTagShouldAssign()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid tagId = Guid.NewGuid();

            note.Apply(new Events.V1.NoteTagAssigned
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                TagId = tagId
            });

            Assert.Equal(tagId, note.Tags.Single().TagId);
        }

        [Fact]
        public void NoteTagShouldUnassignIfAssigned()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid tagId = Guid.NewGuid();

            note.Apply(new Events.V1.NoteTagAssigned
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                TagId = tagId
            });

            note.Apply(new Events.V1.NoteTagUnassigned
            {
                Id = note.Id,
                CurrentUserId = ownerId,
                TagId = tagId
            });

            Assert.True(note.Tags.Count == 0);
        }

        [Fact]
        public void NoteTagUnassignShouldThrowIfUnassigned()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid tagId = Guid.NewGuid();

            Assert.Throws<InvalidOperationException>(() =>
            {
                note.Apply(new Events.V1.NoteTagUnassigned
                {
                    Id = note.Id,
                    CurrentUserId = ownerId,
                    TagId = tagId
                });
            });
        }

        [Fact]
        public void NoteSnaphotShouldCreate()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            var noteContentBefore = "Initial note content.";

            note.Apply(new Events.V1.NoteUpdated
            {
                Title = note.Title,
                Content = noteContentBefore,
                CurrentUserId = ownerId,
                Id = note.Id
            });

            note.Apply(new Events.V1.NoteSnapshotCreated
            {
                Content = note.Content,
                CurrentUserId = ownerId,
                Id = note.Id
            });
            
            var noteContentAfter = "Updated note content.";

            note.Apply(new Events.V1.NoteUpdated
            {
                Title = note.Title,
                Content = noteContentAfter,
                CurrentUserId = ownerId,
                Id = note.Id
            });

            Assert.Equal(noteContentAfter, note.Content);
            Assert.Equal(noteContentBefore, note.Snapshots.Single().Content);
        }

        [Fact]
        public void NoteSnapshotShouldDelete()
        {
            Guid ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(new Events.V1.NoteCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });


            note.Apply(new Events.V1.NoteUpdated
            {
                Title = note.Title,
                Content = "Note content.",
                CurrentUserId = ownerId,
                Id = note.Id
            });

            note.Apply(new Events.V1.NoteSnapshotCreated
            {
                Content = note.Content,
                CurrentUserId = ownerId,
                Id = note.Id
            });

            var expectedSizeBefore = 1;

            Assert.Equal(expectedSizeBefore, note.Snapshots.Count);


            note.Apply(new Events.V1.NoteSnapshotDeleted
            {
                SnapshotId = note.Snapshots.Single().Id,
                CurrentUserId = ownerId,
                Id = note.Id
            });

            var expectedSizeAfter = 0;

            Assert.Equal(expectedSizeAfter, note.Snapshots.Count);
        }
    }
}
