using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Note
{
    public class NoteContributor : Entity<NoteContributorId>
    {
        //Presistance
        public Guid NoteContributorId { get; private set; }

        //Properties


        //Constructors
        protected NoteContributor() { }
        public NoteContributor(Action<object> applier) : base(applier) { }


        //Methods


        //Entity abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.NoteContributorAdded e:
                    Id = new NoteContributorId(e.ContributorId);
                    break;
            }
        }
    }
}
