using Balto.Domain.Aggregates.Note;
using System;
using Xunit;

namespace Balto.Domain.Tests
{
    public class NoteTests
    {
        [Fact]
        public void NoteCanCreate()
        {
            var ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId), 
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));
        }

        [Fact]
        public void NoteCanUpdate()
        {
            var ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId),
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));

            string title = "New title";
            string content = "New content";

            note.Update(title, content);

            Assert.Equal(note.Title.Value, title);
            Assert.Equal(note.Content.Value, content);
        }

        [Fact]
        public void NoteCanDeleteOwner()
        {
            var ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId),
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));

            note.Delete(ownerId);
        }

        [Fact]
        public void NoteCanNotDeleteContributor()
        {
            var ownerId = Guid.NewGuid();
            var contributorId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId),
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));

            note.AddContributor(contributorId, ownerId);

            Assert.Throws<UnauthorizedAccessException>(() => note.Delete(contributorId));
        }

        [Fact]
        public void NoteCanPublishOwner()
        {
            var ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId),
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));

            note.ChangePublication(ownerId);
        }

        [Fact]
        public void NoteCanNotPublishContributor()
        {
            var ownerId = Guid.NewGuid();
            var contributorId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId),
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));

            note.AddContributor(contributorId, ownerId);

            Assert.Throws<UnauthorizedAccessException>(() => note.ChangePublication(contributorId));
        }

        [Fact]
        public void NoteCanAddContributorAsOwner()
        {
            var ownerId = Guid.NewGuid();
            var contributorId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId),
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));

            note.AddContributor(contributorId, ownerId);
        }

        [Fact]
        public void NoteCanNotAddContributorAsContributor()
        {
            var ownerId = Guid.NewGuid();
            var contributorId = Guid.NewGuid();
            var anotherContributorId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId),
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));

            note.AddContributor(contributorId, ownerId);

            Assert.Throws<UnauthorizedAccessException>(() => note.AddContributor(anotherContributorId, contributorId));
        }

        [Fact]
        public void NoteCanRemoveContributorAsOwner()
        {
            var ownerId = Guid.NewGuid();
            var contributorId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId),
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));

            note.AddContributor(contributorId, ownerId);

            note.DeleteContributor(contributorId, ownerId);
        }

        [Fact]
        public void NoteCanNotRemoveContributorAsContributor()
        {
            var ownerId = Guid.NewGuid();
            var contributorId = Guid.NewGuid();
            var anotherContributorId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId),
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));

            note.AddContributor(contributorId, ownerId);

            note.AddContributor(anotherContributorId, ownerId);

            Assert.Throws<UnauthorizedAccessException>(() => note.AddContributor(anotherContributorId, contributorId));
        }

        [Fact]
        public void NoteContributorCanLeave()
        {
            var ownerId = Guid.NewGuid();
            var contributorId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId),
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));

            note.AddContributor(contributorId, ownerId);

            note.Leave(contributorId);
        }

        [Fact]
        public void NoteOwnerCanNotLeave()
        {
            var ownerId = Guid.NewGuid();

            var note = Note.Factory.Create(
                new NoteOwnerId(ownerId),
                NoteTitle.FromString("Note title"),
                NoteContent.FromString("<h1>Some note content</h1>"));

            Assert.Throws<InvalidOperationException>(() => note.Leave(ownerId));
        }
    }
}