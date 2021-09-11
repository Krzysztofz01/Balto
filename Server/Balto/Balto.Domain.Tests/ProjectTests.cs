using Balto.Domain.Aggregates.Project;
using System;
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
    }
}
