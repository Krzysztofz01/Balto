using Balto.Domain.Common;
using Balto.Domain.Exceptions;
using Balto.Domain.Extensions;
using System;

namespace Balto.Domain.Aggregates.Objective
{
    public class Objective : AggregateRoot<ObjectiveId>
    {
        //Presistance
        public Guid ObjectiveId { get; private set; }


        //Properties
        public ObjectiveTitle Title { get; private set; }
        public ObjectiveDescription Description { get; private set; }
        public ObjectivePriority Priority { get; private set; }
        public ObjectivePeriodicity Periodicity { get; private set; }
        public ObjectiveStartingDate StartingDate { get; private set; }
        public ObjectiveEndingDate EndingDate { get; private set; }
        public bool Finished { get; private set; }
        public ObjectiveFinishDate FinishDate { get; private set; }
        public ObjectiveOwnerId OwnerId { get; set; }


        //Constructors
        protected Objective() { }
        protected Objective(
                ObjectiveOwnerId ownerId,
                ObjectiveTitle title,
                ObjectiveDescription description,
                ObjectivePriority priority,
                ObjectivePeriodicity periodicity,
                ObjectiveStartingDate startingDate,
                ObjectiveEndingDate endingDate)
            {
            Apply(new Events.ObjectiveCreated
            {
                Id = Guid.NewGuid(),
                OwnerId = ownerId,
                Title = title,
                Description = description,
                Priority = priority,
                Periodicity = periodicity,
                StartingDate = startingDate,
                EndingDate = endingDate
            });
        }


        //Methods
        public void Update(string title, string description, ObjectivePriorityType priority) =>
            Apply(new Events.ObjectiveInformationsChanged
            {
                Title = title,
                Description = description,
                Priority = priority
            });

        public void Delete() =>
            Apply(new Events.ObjectiveDeleted
            {
                Id = Id
            });

        public void ChangeState() =>
            Apply(new Events.ObjectiveFinishStateChanged
            {
                Id = Id
            });

        public void ResetState() =>
            Apply(new Events.ObjectiveStateReset
            {
                Id = Id
            });


        //Aggregate root abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.ObjectiveCreated e:
                    Id = new ObjectiveId(e.Id);
                    Title = ObjectiveTitle.FromString(e.Title);
                    Description = ObjectiveDescription.FromString(e.Description);
                    Priority = ObjectivePriority.Set(e.Priority);        
                    Finished = false;
                    FinishDate = ObjectiveFinishDate.Unfinished;
                    OwnerId = new ObjectiveOwnerId(e.OwnerId);

                    Periodicity = ObjectivePeriodicity.Set(e.Periodicity);
                    if (Periodicity == ObjectivePeriodicityType.Default)
                    {
                        StartingDate = ObjectiveStartingDate.Set(e.StartingDate);
                        EndingDate = ObjectiveEndingDate.Set(e.EndingDate);
                    }
                    else
                    {
                        StartingDate = ObjectiveStartingDate.Now;

                        if (Periodicity == ObjectivePeriodicityType.Daily)
                            EndingDate = ObjectiveEndingDate.Set(DateTime.Today.DayEndToday());

                        if (Periodicity == ObjectivePeriodicityType.Weekly)
                            EndingDate = ObjectiveEndingDate.Set(DateTime.Today.DayEndWeek());
                    }
                    break;

                case Events.ObjectiveInformationsChanged e:
                    Title = ObjectiveTitle.FromString(e.Title);
                    Description = ObjectiveDescription.FromString(e.Description);
                    Priority = ObjectivePriority.Set(e.Priority);
                    break;

                case Events.ObjectiveFinishStateChanged _:
                    Finished = !Finished;
                    FinishDate = (Finished) ? ObjectiveFinishDate.Set(DateTime.Now) : ObjectiveFinishDate.Unfinished;
                    break;

                case Events.ObjectiveDeleted _:
                    SetAsDeleted();
                    break;

                case Events.ObjectiveStateReset _:
                    if (Periodicity == ObjectivePeriodicityType.Default)
                        throw new InvalidEntityStateException(this, "Default periodicity objectives can not be reset.");

                    Finished = false;
                    FinishDate = ObjectiveFinishDate.Unfinished;
                    StartingDate = ObjectiveStartingDate.Now;

                    if (Periodicity == ObjectivePeriodicityType.Daily)
                        EndingDate = ObjectiveEndingDate.Set(DateTime.Today.DayEndToday());

                    if (Periodicity == ObjectivePeriodicityType.Weekly)
                        EndingDate = ObjectiveEndingDate.Set(DateTime.Today.DayEndWeek());
                    break;
            }
        }

        protected override void EnsureValidState()
        {
            //Null check
            bool valid = Id != null && OwnerId != null &&
                Title != null && StartingDate != null && EndingDate != null && Description != null && Priority != null;

            if (!valid) 
                throw new InvalidEntityStateException(this, "Final property validation failed.");

            //Validate if the starting date is smaller than the ending date
            if (StartingDate.Value > EndingDate.Value) 
                throw new InvalidEntityStateException(this, "Starting date must be smaller than ending date.");

            //Validate finish date for finished objective
            if ((Finished && FinishDate.Value == null) || (!Finished && FinishDate.Value != null))
                throw new InvalidEntityStateException(this, "Finished objective need to have a finish date.");
        }


        //Factory
        public static class Factory
        {
            public static Objective Create(
                ObjectiveOwnerId ownerId,
                ObjectiveTitle title,
                ObjectiveDescription description,
                ObjectivePriority priority,
                ObjectivePeriodicity periodicity,
                ObjectiveStartingDate startingDate,
                ObjectiveEndingDate endingDate)
            {
                return new Objective(ownerId, title, description, priority, periodicity, startingDate, endingDate);
            }
        }
    }
}
