using Balto.Domain.Aggregates.Objective;
using System;
using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Aggregates.Objectives
{
    public static class Commands
    {
        public static class V1
        {
            public class ObjectiveAdd
            {
                [Required]
                public string Title { get; set; }

                [Required]
                public string Description { get; set; }

                [Required]
                public ObjectivePriorityType Priority { get; set; }

                [Required]
                public ObjectivePeriodicityType Periodicity { get; set; }

                [Required]
                public DateTime StartingDate { get; set; }

                [Required]
                public DateTime EndingDate { get; set; }
            }

            public class ObjectiveDelete
            {
                [Required]
                public Guid TargetObjectiveId { get; set; }
            }

            public class ObjectiveUpdate
            {
                [Required]
                public Guid TargetObjectiveId { get; set; }

                [Required]
                public string Title { get; set; }

                [Required]
                public string Description { get; set; }

                [Required]
                public ObjectivePriorityType Priority { get; set; }
            }

            public class ObjectiveStateChange
            {
                [Required]
                public Guid TargetObjectiveId { get; set; }
            }
        }
    }
}
