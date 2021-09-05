using System;

namespace Balto.Application.Aggregates.Objectives
{
    public static class Dto
    {
        public static class V1
        {
            public class ObjectiveDetails
            {
                public Guid Id { get; set; }
                public Guid OwnerId { get; set; }
                public string Title { get; set; }
                public string Description { get; set; }
                public int Priority { get; set; }
                public int Periodicity { get; set; }
                public DateTime StartingDate { get; set; }
                public DateTime EndingDate { get; set; }
                public bool Finished { get; set; }
                public DateTime? FinishDate { get; set; }
            }

            public class ObjectiveSimple
            {
                public Guid Id { get; set; }
                public Guid OwnerId { get; set; }
                public string Title { get; set; }
                public int Priority { get; set; }
                public int Periodicity { get; set; }
                public DateTime StartingDate { get; set; }
                public DateTime EndingDate { get; set; }
                public bool Finished { get; set; }
            }
        }
    }
}
