using System;
using System.Collections.Generic;

namespace Balto.Application.Aggregates.Project
{
    public static class Dto
    {
        public static class V1
        {
            public class ProjectDetails
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
                public Guid OwnerId { get; set; }
                public string TicketToken { get; set; }
                public IEnumerable<Guid> ContributorsIds { get; set; }
                public IEnumerable<TableDetails> Tables { get; set; }
            }

            public class ProjectSimple
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
            }

            public class TableDetails
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
                public string Color { get; set; }
                public IEnumerable<CardSimple> Cards { get; set; }
            }

            public class TableSimple
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
                public string Color { get; set; }
            }

            public class CardDetails
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
                public string Content { get; set; }
                public string Color { get; set; }
                public Guid CreatorId { get; set; }
                public DateTime StartingDate { get; set; }
                public DateTime? DeadlineDate { get; set; }
                public Guid? AssignedUserId { get; set; }
                public bool Notify { get; set; }
                public bool Finished { get; set; }
                public Guid? FinishedByUserId { get; set; }
                public DateTime? FinishDate { get; set; }
                public int Priority { get; set; }
                public int OrdinalNumber { get; set; }
                public IEnumerable<CommentDetails> Comments { get; set; }
            }

            public class CardSimple
            {
                public Guid Id { get; set; }
                public string Title { get; set; }
                public string Color { get; set; }
                public Guid CreatorId { get; set; }
                public DateTime? DeadlineDate { get; set; }
                public Guid? AssignedUserId { get; set; }
                public bool Finished { get; set; }
                public Guid? FinishedByUserId { get; set; }
                public int Priority { get; set; }
                public int OrdinalNumber { get; set; }
            }

            public class CommentDetails
            {
                public Guid Id { get; set; }
                public string Content { get; set; }
                public Guid UserId { get; set; }
                public DateTime CreateDate { get; set; }
            }
        }
    }
}
