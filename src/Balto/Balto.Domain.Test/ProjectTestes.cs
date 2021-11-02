using Balto.Domain.Projects;
using System;
using System.Linq;
using Xunit;

namespace Balto.Domain.Test
{
    public class ProjectTestes
    {
        [Fact]
        public void ProjectShouldCreate()
        {
            Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = Guid.NewGuid()
            });
        }

        [Fact]
        public void ProjectShouldUpdate()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectUpdated
            {
                Id = project.Id,
                CurrentUserId = ownerId,
                TicketStatus = null,
                Title = "New example title"
            });

            string expected = "New example title";

            Assert.Equal(expected, project.Title);
        }

        [Fact]
        public void ProjectShouldThrowOnUpdateWithoutPermission()
        {
            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = Guid.NewGuid()
            });

            Assert.Throws<InvalidOperationException>(() => project.Apply(new Events.V1.ProjectUpdated
            {
                Id = project.Id,
                CurrentUserId = Guid.NewGuid(),
                TicketStatus = null,
                Title = "New example title"
            }));
        }

        [Fact]
        public void ProjectShouldDelete()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectDeleted
            {
                Id = project.Id,
                CurrentUserId = ownerId
            });

            DateTime? expected = DateTime.Now;

            Assert.Equal(expected.Value.Date, project.DeletedAt.Value.Date);
        }

        [Fact]
        public void ProjectShouldThrowOnDeleteWithoutPermission()
        {
            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = Guid.NewGuid()
            });

            Assert.Throws<InvalidOperationException>(() => project.Apply(new Events.V1.ProjectDeleted
            {
                Id = project.Id,
                CurrentUserId = Guid.NewGuid()
            }));
        }

        [Fact]
        public void ProjectShouldPushTicketWhenEnabled()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ProjectShouldThrowOnTicketPushWhenDisabled()
        {
            throw new NotImplementedException();
        }

        [Fact]
        public void ProjectContributorShouldBeAdded()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            project.Apply(new Events.V1.ProjectContributorAdded
            {
                Id = project.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            var expected = conId;

            var actual = project.Contributors.Single().IdentityId.Value;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProjectContributorShouldThrowOnAddWithoutPermission()
        {
            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = Guid.NewGuid()
            });

            Assert.Throws<InvalidOperationException>(() => project.Apply(new Events.V1.ProjectContributorAdded
            {
                Id = project.Id,
                CurrentUserId = Guid.NewGuid(),
                UserId = Guid.NewGuid()
            }));
        }

        [Fact]
        public void ProjectContributorShouldThrowOnAddingCreator()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Assert.Throws<InvalidOperationException>(() => project.Apply(new Events.V1.ProjectContributorAdded
            {
                Id = project.Id,
                CurrentUserId = ownerId,
                UserId = ownerId
            }));
        }

        [Fact]
        public void ProjectContributorShouldThrowOnAddingKnownContributor()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            project.Apply(new Events.V1.ProjectContributorAdded
            {
                Id = project.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            Assert.Throws<InvalidOperationException>(() => project.Apply(new Events.V1.ProjectContributorAdded
            {
                Id = project.Id,
                CurrentUserId = ownerId,
                UserId = conId
            }));
        }

        [Fact]
        public void ProjectContributorShouldBeDeleted()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            project.Apply(new Events.V1.ProjectContributorAdded
            {
                Id = project.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            int exptectedCount = 1;

            Assert.Equal(exptectedCount, project.Contributors.Count);

            project.Apply(new Events.V1.ProjectContributorDeleted
            {
                Id = project.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            int exptectedCountAfter = 0;

            Assert.Equal(exptectedCountAfter, project.Contributors.Count);
        }

        [Fact]
        public void ProjectContributorDeleteShouldThrowWithoutPermission()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            project.Apply(new Events.V1.ProjectContributorAdded
            {
                Id = project.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            int exptectedCount = 1;

            Assert.Equal(exptectedCount, project.Contributors.Count);

            Assert.Throws<InvalidOperationException>(() => project.Apply(new Events.V1.ProjectContributorDeleted
            {
                Id = project.Id,
                CurrentUserId = Guid.NewGuid(),
                UserId = conId
            }));
        }

        [Fact]
        public void ProjectContributorShouldLeftWhenContributor()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Guid conId = Guid.NewGuid();

            project.Apply(new Events.V1.ProjectContributorAdded
            {
                Id = project.Id,
                CurrentUserId = ownerId,
                UserId = conId
            });

            int exptectedCount = 1;

            Assert.Equal(exptectedCount, project.Contributors.Count);

            project.Apply(new Events.V1.ProjectContributorLeft
            {
                Id = project.Id,
                CurrentUserId = conId
            });

            int exptectedCountAfter = 0;

            Assert.Equal(exptectedCountAfter, project.Contributors.Count);
        }

        [Fact]
        public void ProjectContributorShouldThrowWhenOwner()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Assert.Throws<InvalidOperationException>(() => project.Apply(new Events.V1.ProjectContributorLeft
            {
                Id = project.Id,
                CurrentUserId = ownerId
            }));
        }
    }
}
