using Balto.Application.Abstraction;
using Balto.Domain.Goals;
using Balto.Domain.Shared;
using System;

namespace Balto.Application.Goals
{
    public static class Commands
    {
        public static class V1
        {
            public class Create : IApplicationCommand<Goal>
            {
                public string Title { get; set; }
            }

            public class Delete : IApplicationCommand<Goal>
            {
                public Guid Id { get; set; }
            }

            public class Update : IApplicationCommand<Goal>
            {
                public Guid Id { get; set; }

                public string Title { get; set; }

                public string Description { get; set; }

                public PriorityTypes? Priority { get; set; }

                public string Color { get; set; }

                public DateTime? StartingDate { get; set; }

                public DateTime? Deadline { get; set; }

                public bool? IsRecurring { get; set; }
            }

            public class StatusChange : IApplicationCommand<Goal>
            {
                public Guid Id { get; set; }

                public bool? State { get; set; }
            }

            public class TagAssign : IApplicationCommand<Goal>
            {
                public Guid Id { get; set; }
            
                public Guid TagId { get; set; }
            }

            public class TagUnassign : IApplicationCommand<Goal>
            {
                public Guid Id { get; set; }

                public Guid TagId { get; set; }
            }
        }
    }
}
