using Balto.Domain.Core.Events;
using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;
using System;
using static Balto.Domain.Goals.Events;

namespace Balto.Domain.Goals
{
    public class Goal : AggregateRoot
    {
        public Guid OwnerId { get; init; }

        public GoalTitle Title { get; private set; }
        public GoalDescription Description { get; private set; }
        public GoalPriority Priority { get; private set; }
        public GoalColor Color { get; private set; }
        public GoalStartingDate StartingDate { get; private set; }
        public GoalDeadline Deadline { get; private set; }
        public GoalIsRecurring IsRecurring { get; private set; }
        public GoalStatus Status { get; private set; }

        protected override void Handle(IEventBase @event)
        {
            switch(@event)
            {
                case V1.GoalUpdated e: When(e); break;
                case V1.GoalDeleted e: When(e); break;
                case V1.GoalStateChanged e: When(e); break;
                case V1.GoalRecurringReset e: When(e); break;
                default: throw new BusinessLogicException("This entity can not handle this type of event.");
            }
        }

        protected override void Validate()
        {
            bool isNull = Title == null || Description == null || Priority == null ||
                Color == null || StartingDate == null || Deadline == null || IsRecurring == null || Status == null;

            if (isNull)
                throw new BusinessLogicException("The goal aggregate properties can not be null.");
        }

        private void When(V1.GoalUpdated @event)
        {
            Title = GoalTitle.FromString(@event.Title);
            Description = GoalDescription.FromString(@event.Description);
            Priority = GoalPriority.FromPriorityTypes(@event.Priority);
            Color = GoalColor.FromString(@event.Color);
            StartingDate = GoalStartingDate.FromDateTime(@event.StartingDate);

            Deadline = (@event.Deadline.HasValue) ?
                GoalDeadline.FromDateTime(@event.Deadline.Value) : GoalDeadline.NoDeadline;

            if (IsRecurring)
            {
                if (@event.IsRecurring)
                {
                    bool persistStatus = false;

                    if (Status.Finished)
                    {
                        if (Status.FinishDate.Value.Date == DateTime.Now.Date)
                        {
                            persistStatus = true;
                        }
                    }

                    Status = GoalStatus.FromBool(persistStatus);

                    StartingDate = GoalStartingDate.FromDateTime(DateTime.Now.Start());
                    Deadline = GoalDeadline.FromDateTime(DateTime.Now.End());
                }
                else
                {
                    Status = GoalStatus.FromBool(false);

                    StartingDate = GoalStartingDate.Now;
                    Deadline = GoalDeadline.NoDeadline;
                }
            }

            IsRecurring = GoalIsRecurring.FromBool(@event.IsRecurring);
        }

        private void When(V1.GoalDeleted _)
        {
            DeletedAt = DateTime.Now;
        }

        private void When(V1.GoalStateChanged @event)
        {
            Status = GoalStatus.FromBool(@event.State);
        }

        private void When(V1.GoalRecurringReset _)
        {
            Status = GoalStatus.FromBool(false);

            StartingDate = GoalStartingDate.FromDateTime(DateTime.Now.Start());
            Deadline = GoalDeadline.FromDateTime(DateTime.Now.End());
        }

        private Goal() { }

        public static class Factory
        {
            public static Goal Create(V1.GoalCreated @event)
            {
                return new Goal
                {
                    OwnerId = @event.CurrentUserId,
                    Title = GoalTitle.FromString(@event.Title),
                    Description = GoalDescription.Empty,
                    Priority = GoalPriority.Default,
                    Color = GoalColor.Default,
                    StartingDate = GoalStartingDate.Now,
                    Deadline = GoalDeadline.NoDeadline,
                    IsRecurring = GoalIsRecurring.FromBool(false),
                    Status = GoalStatus.FromBool(false)
                };
            }
        }
    }
}
