using Balto.Domain.Common;
using Balto.Domain.Exceptions;
using System;

namespace Balto.Domain.Aggregates.Team
{
    public class Team : AggregateRoot<TeamId>
    {
        //Persistence
        public Guid TeamId { get; private set; }

        //Properties
        public TeamName Name { get; private set; }
        public TeamColor Color { get; private set; }


        //Constructors
        protected Team() { }
        protected Team(string name, string color) =>
            Apply(new Events.TeamCreated
            {
                Id = Guid.NewGuid(),
                Name = name,
                Color = color
            });


        //Methods
        public void Update(string name, string color) =>
            Apply(new Events.TeamUpdated
            {
                Id = Id,
                Name = name,
                Color = color
            });

        public void Delete() =>
            Apply(new Events.TeamDeleted
            {
                Id = Id
            });


        //Aggregate root abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.TeamCreated e:
                    Id = new TeamId(e.Id);
                    Name = TeamName.FromString(e.Name);
                    Color = TeamColor.Set(e.Color);
                    break;

                case Events.TeamUpdated e:
                    Name = TeamName.FromString(e.Name);
                    Color = TeamColor.Set(e.Color);
                    break;

                case Events.TeamDeleted _:
                    SetAsDeleted();
                    break;
            }
        }

        protected override void EnsureValidState()
        {
            bool valid = Id != null &&
                Name != null & Color != null;

            if (!valid)
                throw new InvalidEntityStateException(this, "Final property validation failed.");
        }


        //Factory
        public static class Factory
        {
            public static Team Create(string name, string color)
            {
                return new Team(name, color);
            }
        }
    }
}
