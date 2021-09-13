using Balto.Domain.Aggregates.Project;
using Balto.Domain.Aggregates.Project.Card;
using System;
using System.Linq;
using Xunit;

namespace Balto.Domain.Tests
{
    public class ProjectTests
    {
        [Fact]
        public void ProjectShouldCreate()
        {
            var userId = Guid.NewGuid();

            Project.Factory.Create(new ProjectOwnerId(userId), ProjectTitle.FromString("Test project"));
        }

        [Fact]
        public void ProjectShouldUpdateAccordingToAccessRules()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var randomUserId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            var newTitle = "New title";

            Assert.Throws<UnauthorizedAccessException>(() => project.Update(newTitle, contributorId));

            Assert.Throws<UnauthorizedAccessException>(() => project.Update(newTitle, randomUserId));

            project.Update(newTitle, ownerId);

            Assert.Equal(project.Title.Value, newTitle);
        }

        [Fact]
        public void ProjectShouldDeleteAccordingToAccessRules()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var randomUserId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            Assert.Throws<UnauthorizedAccessException>(() => project.Delete(contributorId));

            Assert.Throws<UnauthorizedAccessException>(() => project.Delete(randomUserId));

            project.Delete(ownerId);
        }

        [Fact]
        public void ProjectShouldGetNewContributorAccordingToAccessRules()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var randomUserId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            var anotherUser = Guid.NewGuid();

            Assert.Throws<UnauthorizedAccessException>(() => project.AddContributor(anotherUser, contributorId));

            Assert.Throws<UnauthorizedAccessException>(() => project.AddContributor(anotherUser, randomUserId));

            project.AddContributor(anotherUser, ownerId);
        }

        [Fact]
        public void ProjectShouldLostNewContributorAccordingToAccessRules()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var randomUserId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            var anotherUser = Guid.NewGuid();

            project.AddContributor(anotherUser, ownerId);

            Assert.Throws<UnauthorizedAccessException>(() => project.DeleteContributor(anotherUser, contributorId));

            Assert.Throws<UnauthorizedAccessException>(() => project.DeleteContributor(anotherUser, randomUserId));

            project.DeleteContributor(anotherUser, ownerId);
        }

        [Fact]
        public void ProjectCanBeLeftAccordingToAccessRules()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var randomUserId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            project.Leave(contributorId);

            Assert.Throws<InvalidOperationException>(() => project.Leave(randomUserId));

            Assert.Throws<InvalidOperationException>(() => project.Leave(ownerId));
        }

        [Fact]
        public void ProjectTicketTokenStatusCanBeChangedAccordingToAccessRule()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var randomUserId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            Assert.Throws<UnauthorizedAccessException>(() => project.ChangeTicketTokenStatus(contributorId));

            Assert.Throws<UnauthorizedAccessException>(() => project.ChangeTicketTokenStatus(randomUserId));

            project.ChangeTicketTokenStatus(ownerId);
        }

        [Fact]
        public void ProjectTicketCanBeCreated()
        {
            var ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.ChangeTicketTokenStatus(ownerId);

            string ticketTitle = "Some ticket";
            string ticketContent = "Some ticket content";

            project.AddTicket(ticketTitle, ticketContent);

            int expectedSizeOfElements = 1;

            int actualAmountOfElements = project
                .Tables.Single(s => s.Title.Value == "Tickets")
                .Cards.Where(s => s.Title.Value == ticketTitle && s.Content.Value == ticketContent)
                .Count();

            Assert.Equal(expectedSizeOfElements, actualAmountOfElements);
        }

        [Fact]
        public void ProjectTableCanBeAddedAccordingToAccessRule()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var randomUserId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            Assert.Throws<UnauthorizedAccessException>(() => project.AddTable("Some title", contributorId));

            Assert.Throws<UnauthorizedAccessException>(() => project.AddTable("Some title", randomUserId));

            project.AddTable("Some title", ownerId);
        }

        [Fact]
        public void ProjectTableCanBeUpdatedAccordingToAccessRule()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var randomUserId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            string tableName = "Some title";

            project.AddTable(tableName, ownerId);

            var tableId = project.Tables.Single(e => e.Title.Value == tableName).Id.Value;

            Assert.Throws<UnauthorizedAccessException>(() => project.UpdateTable(tableId, "New title", "#123ABC", contributorId));

            Assert.Throws<UnauthorizedAccessException>(() => project.UpdateTable(tableId, "New title", "#123ABC", randomUserId));

            project.UpdateTable(tableId, "New title", "#123ABC", ownerId);
        }

        [Fact]
        public void ProjectTableCanBeDeletedAccordingToAccessRule()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var randomUserId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            string tableName = "Some title";

            project.AddTable(tableName, ownerId);

            var tableId = project.Tables.Single(e => e.Title.Value == tableName).Id.Value;

            Assert.Throws<UnauthorizedAccessException>(() => project.DeleteTable(tableId, contributorId));

            Assert.Throws<UnauthorizedAccessException>(() => project.DeleteTable(tableId, randomUserId));

            project.DeleteTable(tableId, ownerId);
        }

        [Fact]
        public void ProjectTableCardCanBeCreatedAccordingToAccessRule()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            string tableName = "Some title";

            project.AddTable(tableName, ownerId);

            var tableId = project.Tables.Single(e => e.Title.Value == tableName).Id.Value;

            project.AddCard(tableId, "My card", ownerId);

            project.AddCard(tableId, "My card", contributorId);
        }

        [Fact]
        public void ProjectTableCardCanBeUpdatedAccordingToAccessRule()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            string tableName = "Some title";

            project.AddTable(tableName, ownerId);

            var tableId = project.Tables.Single(e => e.Title.Value == tableName).Id.Value;

            string cardName = "My card";

            project.AddCard(tableId, cardName, ownerId);

            var cardId = project.Tables.Single(e => e.Title.Value == tableName).Cards.Single(e => e.Title.Value == cardName).Id.Value;

            project.UpdateCard(cardId, "New title", "New content", "#123ABC", DateTime.Now, true, DateTime.Now.AddDays(2), contributorId, CardPriorityType.Important);
        }

        [Fact]
        public void ProjectTableCardCanBeDeletedAccordingToAccessRule()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            string tableName = "My table";

            project.AddTable(tableName, ownerId);

            var tableId = project.Tables.Single(e => e.Title.Value == tableName).Id.Value;

            string ownersCardName = "Owners card";
            string contributorsCardName = "Contributors card";
            string anotherContributorsCardName = "Another contributors card";

            project.AddCard(tableId, ownersCardName, ownerId);
            project.AddCard(tableId, contributorsCardName, contributorId);
            project.AddCard(tableId, anotherContributorsCardName, contributorId);

            var ownersCardId = project.Tables.Single(e => e.Title.Value == tableName).Cards.Single(e => e.Title.Value == ownersCardName).Id.Value;
            var contributorsCardId = project.Tables.Single(e => e.Title.Value == tableName).Cards.Single(e => e.Title.Value == contributorsCardName).Id.Value;
            var anotherContributorsCardId = project.Tables.Single(e => e.Title.Value == tableName).Cards.Single(e => e.Title.Value == anotherContributorsCardName).Id.Value;

            Assert.Throws<UnauthorizedAccessException>(() => project.DeleteCard(ownersCardId, contributorId));

            project.DeleteCard(contributorsCardId, contributorId);

            project.DeleteCard(anotherContributorsCardId, ownerId);
            project.DeleteCard(ownersCardId, ownerId);
        }

        [Fact]
        public void ProjectTableCardStatusCanBeChangedAccordingToAccessRule()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            string tableName = "My table";

            project.AddTable(tableName, ownerId);

            var tableId = project.Tables.Single(e => e.Title.Value == tableName).Id.Value;

            string cardName = "Owners card";

            project.AddCard(tableId, cardName, ownerId);

            var cardId = project.Tables.Single(e => e.Title.Value == tableName).Cards.Single(e => e.Title.Value == cardName).Id.Value;

            project.ChangeCardStatus(cardId, ownerId);
            project.ChangeCardStatus(cardId, contributorId);
        }

        [Fact]
        public void ProjectTableCardCommentCanBeAddedAccordingToAccessRule()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            string tableName = "My table";

            project.AddTable(tableName, ownerId);

            var tableId = project.Tables.Single(e => e.Title.Value == tableName).Id.Value;

            string cardName = "Owners card";

            project.AddCard(tableId, cardName, ownerId);

            var cardId = project.Tables.Single(e => e.Title.Value == tableName).Cards.Single(e => e.Title.Value == cardName).Id.Value;

            project.AddCommentToCard(cardId, "My comment", ownerId);
            project.AddCommentToCard(cardId, "My comment", contributorId);
        }

        [Fact]
        public void ProjectTableCardCommentCanBeDeletedAccordingToAccessRule()
        {
            var ownerId = Guid.NewGuid();

            var contributorId = Guid.NewGuid();

            var project = Project.Factory.Create(new ProjectOwnerId(ownerId), ProjectTitle.FromString("Test project"));

            project.AddContributor(contributorId, ownerId);

            string tableName = "My table";

            project.AddTable(tableName, ownerId);

            var tableId = project.Tables.Single(e => e.Title.Value == tableName).Id.Value;

            string cardName = "Owners card";

            project.AddCard(tableId, cardName, ownerId);

            var cardId = project.Tables.Single(e => e.Title.Value == tableName).Cards.Single(e => e.Title.Value == cardName).Id.Value;

            string ownerComment = "Comment owner";
            string contributorComment = "Comment contributor";

            project.AddCommentToCard(cardId, ownerComment, ownerId);
            project.AddCommentToCard(cardId, contributorComment, contributorId);

            var ownerCommentId = project.Tables.Single(e => e.Title.Value == tableName).Cards.Single(e => e.Title.Value == cardName)
                .Comments.Single(c => c.Content.Value == ownerComment).Id.Value;

            var contributorCommentId = project.Tables.Single(e => e.Title.Value == tableName).Cards.Single(e => e.Title.Value == cardName)
                .Comments.Single(c => c.Content.Value == contributorComment).Id.Value;

            Assert.Throws<InvalidOperationException>(() => project.DeleteCommentFromCard(ownerCommentId, contributorId));
            Assert.Throws<InvalidOperationException>(() => project.DeleteCommentFromCard(contributorCommentId, ownerId));

            project.DeleteCommentFromCard(ownerCommentId, ownerId);
            project.DeleteCommentFromCard(contributorCommentId, contributorId);
        }
    }
}
