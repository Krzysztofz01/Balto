using Balto.Application.Abstraction;
using Balto.Domain.Goals;
using Balto.Domain.Shared;
using System;
using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Goals
{
    public static class Commands
    {
        public static class V1
        {
            public class Create : IApplicationCommand<Goal>
            {
                [Required]
                public string Title { get; set; }
            }

            public class Delete : IApplicationCommand<Goal>
            {
                [Required]
                public Guid Id { get; set; }
            }

            public class Update : IApplicationCommand<Goal>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public string Title { get; set; }

                [Required]
                public string Description { get; set; }

                [Required]
                public PriorityTypes Priority { get; set; }

                [Required]
                public string Color { get; set; }

                [Required]
                public DateTime StartingDate { get; set; }

                public DateTime? Deadline { get; set; }

                [Required]
                public bool IsRecurring { get; set; }
            }

            public class StatusChange : IApplicationCommand<Goal>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public bool State { get; set; }
            }
        }
    }
}
