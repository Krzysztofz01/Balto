using Balto.Domain.Aggregates.Objective;
using System;
using Xunit;

namespace Balto.Domain.Tests
{
    public class ObjectiveTests
    {
        [Fact]
        public void ObjectiveDefaultShouldCreate()
        {
            var objective = Objective.Factory.Create(
                new ObjectiveOwnerId(Guid.NewGuid()),
                ObjectiveTitle.FromString("Some objective"),
                ObjectiveDescription.FromString("My new objective details."),
                ObjectivePriority.Default,
                ObjectivePeriodicity.Set(ObjectivePeriodicityType.Default),
                ObjectiveStartingDate.Now,
                ObjectiveEndingDate.Set(DateTime.Now.AddDays(2)));
        }

        [Fact]
        public void ObjectiveDailyShouldCreate()
        {
            var objective = Objective.Factory.Create(
                new ObjectiveOwnerId(Guid.NewGuid()),
                ObjectiveTitle.FromString("Some objective"),
                ObjectiveDescription.FromString("My new objective details."),
                ObjectivePriority.Default,
                ObjectivePeriodicity.Set(ObjectivePeriodicityType.Daily),
                ObjectiveStartingDate.Now,
                ObjectiveEndingDate.Set(DateTime.Now.AddDays(2)));
        }

        [Fact]
        public void ObjectiveWeeklyShouldCreate()
        {
            var objective = Objective.Factory.Create(
                new ObjectiveOwnerId(Guid.NewGuid()),
                ObjectiveTitle.FromString("Some objective"),
                ObjectiveDescription.FromString("My new objective details."),
                ObjectivePriority.Default,
                ObjectivePeriodicity.Set(ObjectivePeriodicityType.Weekly),
                ObjectiveStartingDate.Now,
                ObjectiveEndingDate.Set(DateTime.Now.AddDays(2)));
        }

        [Fact]
        public void ObjectiveDefaultCanBeFinished()
        {
            var objective = Objective.Factory.Create(
                new ObjectiveOwnerId(Guid.NewGuid()),
                ObjectiveTitle.FromString("Some objective"),
                ObjectiveDescription.FromString("My new objective details."),
                ObjectivePriority.Default,
                ObjectivePeriodicity.Set(ObjectivePeriodicityType.Default),
                ObjectiveStartingDate.Now,
                ObjectiveEndingDate.Set(DateTime.Now.AddDays(2)));

            objective.ChangeState();

            bool firstExpectedValue = true;
            Assert.Equal(firstExpectedValue, objective.FinishState.State);

            objective.ChangeState();

            bool secondExpectedValue = false;
            Assert.Equal(secondExpectedValue, objective.FinishState.State);
        }

        [Fact]
        public void ObjectiveDailyCanBeFinished()
        {
            var objective = Objective.Factory.Create(
                new ObjectiveOwnerId(Guid.NewGuid()),
                ObjectiveTitle.FromString("Some objective"),
                ObjectiveDescription.FromString("My new objective details."),
                ObjectivePriority.Default,
                ObjectivePeriodicity.Set(ObjectivePeriodicityType.Daily),
                ObjectiveStartingDate.Set(DateTime.Today.AddDays(-2)),
                ObjectiveEndingDate.Set(DateTime.Now.AddDays(2)));

            Assert.Equal(objective.StartingDate.Value.Date, DateTime.Now.Date);

            objective.ChangeState();

            bool firstExpectedValue = true;
            Assert.Equal(firstExpectedValue, objective.FinishState.State);

            objective.ChangeState();

            bool secondExpectedValue = false;
            Assert.Equal(secondExpectedValue, objective.FinishState.State);
        }

        [Fact]
        public void ObjectiveWeeklyCanBeFinished()
        {
            var objective = Objective.Factory.Create(
                new ObjectiveOwnerId(Guid.NewGuid()),
                ObjectiveTitle.FromString("Some objective"),
                ObjectiveDescription.FromString("My new objective details."),
                ObjectivePriority.Default,
                ObjectivePeriodicity.Set(ObjectivePeriodicityType.Weekly),
                ObjectiveStartingDate.Set(DateTime.Today.AddDays(-2)),
                ObjectiveEndingDate.Set(DateTime.Now.AddDays(2)));

            Assert.Equal(objective.StartingDate.Value.Date, DateTime.Now.Date);

            objective.ChangeState();

            bool firstExpectedValue = true;
            Assert.Equal(firstExpectedValue, objective.FinishState.State);

            objective.ChangeState();

            bool secondExpectedValue = false;
            Assert.Equal(secondExpectedValue, objective.FinishState.State);
        }

        [Fact]
        public void ObjectiveWrongDatesValueObjectShouldThrowException()
        {
            Assert.Throws<ArgumentException>(() => Objective.Factory.Create(
                new ObjectiveOwnerId(Guid.NewGuid()),
                ObjectiveTitle.FromString("Some objective"),
                ObjectiveDescription.FromString("My new objective details."),
                ObjectivePriority.Default,
                ObjectivePeriodicity.Set(ObjectivePeriodicityType.Daily),
                ObjectiveStartingDate.Set(DateTime.Today.AddDays(2)),
                ObjectiveEndingDate.Set(DateTime.Now.AddDays(-2))));
        }

        [Fact]
        public void ObjectiveInformationsCanBeChanged()
        {
            var objective = Objective.Factory.Create(
                new ObjectiveOwnerId(Guid.NewGuid()),
                ObjectiveTitle.FromString("Some objective"),
                ObjectiveDescription.FromString("My new objective details."),
                ObjectivePriority.Default,
                ObjectivePeriodicity.Set(ObjectivePeriodicityType.Default),
                ObjectiveStartingDate.Now,
                ObjectiveEndingDate.Set(DateTime.Now.AddDays(2)));

            objective.Update(
                ObjectiveTitle.FromString("Somew new objective name"),
                ObjectiveDescription.FromString("Some new objective description"),
                ObjectivePriority.Set(ObjectivePriorityType.Crucial));
        }

        [Fact]
        public void ObjectiveCanBeDeleted()
        {
            var objective = Objective.Factory.Create(
                new ObjectiveOwnerId(Guid.NewGuid()),
                ObjectiveTitle.FromString("Some objective"),
                ObjectiveDescription.FromString("My new objective details."),
                ObjectivePriority.Default,
                ObjectivePeriodicity.Set(ObjectivePeriodicityType.Default),
                ObjectiveStartingDate.Now,
                ObjectiveEndingDate.Set(DateTime.Now.AddDays(2)));

            objective.Delete();
        }
    }
}
