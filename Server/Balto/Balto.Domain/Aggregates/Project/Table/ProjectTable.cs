using Balto.Domain.Aggregates.Project.Card;
using Balto.Domain.Common;
using System;
using System.Collections.Generic;

namespace Balto.Domain.Aggregates.Project.Table
{
    public class ProjectTable : Entity<ProjectTableId>
    {
        //Persistence
        public Guid TableId { get; private set; }

        //Properties
        public ProjectTableTitle Title { get; private set; }
        public ProjectTableColor Color { get; private set; }

        private readonly List<ProjectTableCard> _cards;
        public IReadOnlyCollection<ProjectTableCard> Cards { get; private set; }


        //Constructors
        protected ProjectTable()
        {
            _cards = new List<ProjectTableCard>();
        }

        public ProjectTable(Action<object> applier) : base(applier)
        {
            _cards = new List<ProjectTableCard>();
        }


        //Methods



        //Entity abstraction implementation
        protected override void When(object @event)
        {
            throw new NotImplementedException();
        }
    }
}
