using Balto.Domain.Core.Exceptions;
using Balto.Domain.Goals;
using Balto.Domain.Shared;
using System;
using Xunit;

namespace Balto.Domain.Test
{
    public class GoalTestes
    {
        [Fact]
        public void GoalShouldCreateSucessful()
        {
            Goal.Factory.Create(new Events.V1.GoalCreated
            {
                CurrentUserId = Guid.NewGuid(),
                Title = "My new goal."
            });
        }

        [Fact]
        public void GoalShouldThrowOnCreate()
        {
            Assert.Throws<ValueObjectValidationException>(() => Goal.Factory.Create(new Events.V1.GoalCreated
            {
                CurrentUserId = Guid.NewGuid(),
                Title = ""
            }));
        }

        [Fact]
        public void GoalShouldUpdate()
        {
            var goal = Goal.Factory.Create(new Events.V1.GoalCreated
            {
                CurrentUserId = Guid.NewGuid(),
                Title = "My new goal."
            });

            var updateEvent = new Events.V1.GoalUpdated
            {
                Id = goal.Id,
                Title = "My changed goal name",
                Description = "My changed description.",
                Priority = PriorityTypes.Crucial,
                Color = "#AB12CD",
                StartingDate = DateTime.Now.AddDays(-5),
                Deadline = DateTime.Now.AddDays(10),
                IsRecurring = false
            };

            goal.Apply(updateEvent);

            Assert.Equal(goal.Title, updateEvent.Title);
            Assert.Equal(goal.Description, updateEvent.Description);
            Assert.Equal(goal.Priority, updateEvent.Priority);
            Assert.Equal(goal.Color, updateEvent.Color);
            Assert.Equal(goal.StartingDate.Value.Date, updateEvent.StartingDate.Date);
            Assert.Equal(goal.Deadline, updateEvent.Deadline);
        }

        [Fact]
        public void GoalUpdateShouldThrow()
        {
            var goal = Goal.Factory.Create(new Events.V1.GoalCreated
            {
                CurrentUserId = Guid.NewGuid(),
                Title = "My new goal."
            });

            var updateEvent = new Events.V1.GoalUpdated
            {
                Id = goal.Id,
                Title = null,
                Description = " ",
                Priority = goal.Priority,
                Color = "#AB12CDxxxxx",
                StartingDate = new DateTime(),
                Deadline = new DateTime(),
                IsRecurring = false
            };

            Assert.Throws<ValueObjectValidationException>(() => goal.Apply(updateEvent));
        }

        [Fact]
        public void GoalShouldDelete()
        {
            var goal = Goal.Factory.Create(new Events.V1.GoalCreated
            {
                CurrentUserId = Guid.NewGuid(),
                Title = "My new goal."
            });

            goal.Apply(new Events.V1.GoalDeleted
            {
                Id = goal.Id
            });

            bool expectedHasValue = true;
            DateTime? expectedType = null;

            Assert.Equal(expectedHasValue, goal.DeletedAt.HasValue);
            Assert.NotEqual(expectedType, goal.DeletedAt);
        }

        [Fact]
        public void GoalStateShouldChange()
        {
            var goal = Goal.Factory.Create(new Events.V1.GoalCreated
            {
                CurrentUserId = Guid.NewGuid(),
                Title = "My new goal."
            });

            bool exptectedStatusBefore = false;

            Assert.Equal(exptectedStatusBefore, goal.Status);

            goal.Apply(new Events.V1.GoalStateChanged
            {
                Id = goal.Id,
                State = true
            });

            bool exptectedStatusAfter = true;

            Assert.Equal(exptectedStatusAfter, goal.Status);

            goal.Apply(new Events.V1.GoalStateChanged
            {
                Id = goal.Id,
                State = false
            });

            bool exptectedStatusAfterAfter = false;

            Assert.Equal(exptectedStatusAfterAfter, goal.Status);
        }

        [Fact]
        public void RecurringGoalShouldReset()
        {
            var goal = Goal.Factory.Create(new Events.V1.GoalCreated
            {
                CurrentUserId = Guid.NewGuid(),
                Title = "My new goal."
            });

            goal.Apply(new Events.V1.GoalUpdated
            {
                Id = goal.Id,
                Title = goal.Title,
                Description = goal.Description,
                Priority = goal.Priority,
                Color = goal.Color,
                StartingDate = goal.StartingDate,
                Deadline = goal.Deadline,
                IsRecurring = true
            });

            bool exptectedIsRecurring = true;

            Assert.Equal(exptectedIsRecurring, goal.IsRecurring);

            goal.Apply(new Events.V1.GoalStateChanged
            {
                Id = goal.Id,
                State = true
            });

            bool exptectedStatusBeforeReset = true;

            Assert.Equal(exptectedStatusBeforeReset, goal.Status);

            goal.Apply(new Events.V1.GoalRecurringReset
            {
                Id = goal.Id
            });

            bool exptectedStatusAfterReset = false;

            Assert.Equal(exptectedStatusAfterReset, goal.Status);
        }
    }
}
