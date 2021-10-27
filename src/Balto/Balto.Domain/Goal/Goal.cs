using Balto.Domain.Core.Events;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Goal
{
    public class Goal : AggregateRoot
    {
        public Guid OwnerId { get; private set; }

        public GoalTitle Title { get; private set; }
        public GoalDescription Description { get; private set; }
        public GoalPriority Priority { get; private set; }
        public GoalColor Color { get; private set; }
        public GoalStartingDate StartingDate { get; private set; }
        public GoalDeadline Deadline { get; private set; }
        public GoalIsRecurring IsRecurring { get; private set; }
        public GoalStatus Status { get; private set; }

        protected override void Handle(IEvent @event)
        {
            throw new NotImplementedException();
        }

        protected override void Validate()
        {
            throw new NotImplementedException();
        }

        private Goal() { }

        public static class Factory
        {
            public static Goal Create()
            {
                return null;
            }
        }
    }
}
