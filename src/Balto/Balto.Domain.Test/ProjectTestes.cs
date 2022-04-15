using Balto.Domain.Projects;
using Balto.Domain.Shared;
using System;
using System.Collections.Generic;
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
                TicketStatus = true,
                Title = project.Title
            });

            project.Apply(new Events.V1.TicketPushed
            {
                Id = project.Id,
                Title = "Ticket title",
                Content = "Ticket content"
            });

            int expected = 1;

            int actual = project.Tables.Single(t => t.Title == "Tickets").Tasks.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProjectShouldThrowOnTicketPushWhenDisabled()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Assert.Throws<InvalidOperationException>(() => project.Apply(new Events.V1.TicketPushed
            {
                Id = project.Id,
                Title = "Ticket title",
                Content = "Ticket content"
            }));
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
        public void ProjectContributorLeftShouldThrowWhenOwner()
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

        [Fact]
        public void ProjectTableShouldCreate()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });
        }

        [Fact]
        public void ProjectTableCreateShouldThrowOnRestirctedName()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            Assert.Throws<InvalidOperationException>(() => project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Tickets"
            }));
        }

        [Fact]
        public void PrioejctTableShouldUpdate()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });

            project.Apply(new Events.V1.ProjectTableUpdated
            {
                Id = project.Id,
                TableId = project.Tables.Single().Id,
                Color = "#ababab",
                Title = "Example valid title"
            });
        }

        [Fact]
        public void ProjectTableUpdateShouldThrowOnRestrictedName()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });

            Assert.Throws<InvalidOperationException>(() => project.Apply(new Events.V1.ProjectTableUpdated
            {
                Id = project.Id,
                TableId = project.Tables.Single().Id,
                Color = "#ababab",
                Title = "Tickets"
            }));
        }

        [Fact]
        public void ProjectTableShoudlDelete()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });

            project.Apply(new Events.V1.ProjectTableDeleted
            {
                Id = project.Id,
                TableId = project.Tables.Single().Id,
                CurrentUserId = ownerId
            });

            int expected = 0;

            int actual = project.Tables.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProjectTaskShouldCreate()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });

            Guid tableId = project.Tables.Single().Id;

            project.Apply(new Events.V1.ProjectTaskCreated
            {
                Id = project.Id,
                TableId = tableId,
                CurrentUserId = ownerId,
                Title = "New Task"
            });

            int expected = 1;

            int actual = project.Tables.Single().Tasks.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProjectTaskShouldUpdate()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });

            Guid tableId = project.Tables.Single().Id;

            project.Apply(new Events.V1.ProjectTaskCreated
            {
                Id = project.Id,
                TableId = tableId,
                CurrentUserId = ownerId,
                Title = "New Task"
            });

            Guid taskId = project.Tables.Single().Tasks.Single().Id;

            project.Apply(new Events.V1.ProjectTaskUpdated
            {
                AssignedContributorId = ownerId,
                Content = "Some content",
                Deadline = DateTime.Now.AddDays(10),
                StartingDate = DateTime.Now.AddDays(2),
                Id = project.Id,
                Priority = PriorityTypes.Crucial,
                TableId = tableId,
                TaskId = taskId,
                Title = "Another task"
            });

            //TODO: Value check instead of exception check
        }

        [Fact]
        public void ProjectTaskShouldDelete()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });

            Guid tableId = project.Tables.Single().Id;

            project.Apply(new Events.V1.ProjectTaskCreated
            {
                Id = project.Id,
                TableId = tableId,
                CurrentUserId = ownerId,
                Title = "New Task"
            });

            Guid taskId = project.Tables.Single().Tasks.Single().Id;

            project.Apply(new Events.V1.ProjectTaskDeleted
            {
                Id = project.Id,
                TableId = tableId,
                TaskId = taskId
            });

            int expected = 0;

            int actual = project.Tables.Single().Tasks.Count;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProjectTaskStatusShouldChange()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });

            Guid tableId = project.Tables.Single().Id;

            project.Apply(new Events.V1.ProjectTaskCreated
            {
                Id = project.Id,
                TableId = tableId,
                CurrentUserId = ownerId,
                Title = "New Task"
            });

            Guid taskId = project.Tables.Single().Tasks.Single().Id;

            project.Apply(new Events.V1.ProjectTaskStatusChanged
            {
                Id = project.Id,
                TableId = tableId,
                TaskId = taskId,
                CurrentUserId = ownerId,
                Status = true
            });

            bool expected = true;

            bool actual = project.Tables.Single().Tasks.Single().Status;

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void ProjectTaskOrdinalNumberShouldChange()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });

            Guid tableId = project.Tables.Single().Id;

            project.Apply(new Events.V1.ProjectTaskCreated
            {
                Id = project.Id,
                TableId = tableId,
                CurrentUserId = ownerId,
                Title = "The first task"
            });

            project.Apply(new Events.V1.ProjectTaskCreated
            {
                Id = project.Id,
                TableId = tableId,
                CurrentUserId = ownerId,
                Title = "The second task"
            });

            int expected = 1;

            int actual = project.Tables.Single().Tasks.First().OrdinalNumber;

            Assert.Equal(expected, actual);
       
            var orderMap = new List<Tuple<Guid, int>>();
            
            foreach(var task in project.Tables.Single().Tasks)
            {
                orderMap.Add(new Tuple<Guid, int>(task.Id, task.OrdinalNumber));
            }     

            orderMap.Reverse();

            for(int i = 0; i < orderMap.Count; i++)
            {
                orderMap[i] = new Tuple<Guid, int>(orderMap[i].Item1, i + 1);
            }

            project.Apply(new Events.V1.ProjectTableTasksOrdinalNumbersChanged
            {
                Id = project.Id,
                TableId = tableId,
                IdOrdinalNumberPairs = orderMap
            });

            int expectedAfter = 2;

            int actualAfter = project.Tables.Single().Tasks.First().OrdinalNumber;

            Assert.Equal(expectedAfter, actualAfter);
        }

        [Fact]
        public void ProjectTagShouldAssingToProjectTask()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });

            Guid tableId = project.Tables.Single().Id;

            project.Apply(new Events.V1.ProjectTaskCreated
            {
                Id = project.Id,
                TableId = tableId,
                CurrentUserId = ownerId,
                Title = "New Task"
            });

            Guid taskId = project.Tables.Single().Tasks.Single().Id;

            int expectedBefore = 0;

            int acutalBefore = project.Tables.Single().Tasks.Single().Tags.Count;

            Assert.Equal(expectedBefore, acutalBefore);

            Guid tagId = Guid.NewGuid();

            project.Apply(new Events.V1.ProjectTaskTagAssigned
            {
                Id = project.Id,
                TableId = tableId,
                TaskId = taskId,
                TagId = tagId
            });

            int expectedAfter = 1;

            int actualAfter = project.Tables.Single().Tasks.Single().Tags.Count;

            Assert.Equal(expectedAfter, actualAfter);
        }

        [Fact]
        public void ProjectTagShouldBeUnassignedFromProjectTaskIfAssigned()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });

            Guid tableId = project.Tables.Single().Id;

            project.Apply(new Events.V1.ProjectTaskCreated
            {
                Id = project.Id,
                TableId = tableId,
                CurrentUserId = ownerId,
                Title = "New Task"
            });

            Guid taskId = project.Tables.Single().Tasks.Single().Id;

            Guid tagOneId = Guid.NewGuid();
            Guid tagTwoId = Guid.NewGuid();

            project.Apply(new Events.V1.ProjectTaskTagAssigned
            {
                Id = project.Id,
                TableId = tableId,
                TaskId = taskId,
                TagId = tagOneId
            });

            project.Apply(new Events.V1.ProjectTaskTagAssigned
            {
                Id = project.Id,
                TableId = tableId,
                TaskId = taskId,
                TagId = tagOneId
            });

            project.Apply(new Events.V1.ProjectTaskTagAssigned
            {
                Id = project.Id,
                TableId = tableId,
                TaskId = taskId,
                TagId = tagTwoId
            });

            int expectedBefore = 2;

            int acutalBefore = project.Tables.Single().Tasks.Single().Tags.Count;

            Assert.Equal(expectedBefore, acutalBefore);

            project.Apply(new Events.V1.ProjectTaskTagUnassigned
            {
                Id = project.Id,
                TableId = tableId,
                TaskId = taskId,
                TagId = tagOneId
            });

            int expectedAfter = 1;

            int actualAfter = project.Tables.Single().Tasks.Single().Tags.Count;

            Assert.Equal(expectedAfter, actualAfter);
        }

        [Fact]
        public void ProjectTagShouldThrowWhenUnassigningFromProjectTaskIfUnassigned()
        {
            Guid ownerId = Guid.NewGuid();

            var project = Project.Factory.Create(new Events.V1.ProjectCreated
            {
                Title = "Example title",
                CurrentUserId = ownerId
            });

            project.Apply(new Events.V1.ProjectTableCreated
            {
                Id = project.Id,
                Title = "Exmple table title"
            });

            Guid tableId = project.Tables.Single().Id;

            project.Apply(new Events.V1.ProjectTaskCreated
            {
                Id = project.Id,
                TableId = tableId,
                CurrentUserId = ownerId,
                Title = "New Task"
            });

            Guid taskId = project.Tables.Single().Tasks.Single().Id;

            Guid tagOneId = Guid.NewGuid();
            Guid tagTwoId = Guid.NewGuid();

            project.Apply(new Events.V1.ProjectTaskTagAssigned
            {
                Id = project.Id,
                TableId = tableId,
                TaskId = taskId,
                TagId = tagOneId
            });

            int expectedBefore = 1;

            int acutalBefore = project.Tables.Single().Tasks.Single().Tags.Count;

            Assert.Equal(expectedBefore, acutalBefore);

            Assert.Throws<InvalidOperationException>(() =>
            {
                project.Apply(new Events.V1.ProjectTaskTagUnassigned
                {
                    Id = project.Id,
                    TableId = tableId,
                    TaskId = taskId,
                    TagId = tagTwoId
                });
            });

            int expectedAfter = 1;

            int actualAfter = project.Tables.Single().Tasks.Single().Tags.Count;

            Assert.Equal(expectedAfter, actualAfter);
        }
    }
}
