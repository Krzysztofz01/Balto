using Balto.Domain.Aggregates.Project.Card;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Aggregates.Project
{
    public static class Commands
    {
        public static class V1
        {
            public class ProjectAdd
            {
                [Required]
                public string Title { get; set; }
            }

            public class ProjectUpdate
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public string Title { get; set; }
            }

            public class ProjectDelete
            {
                [Required]
                public Guid Id { get; set; }
            }

            public class ProjectAddContributor
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid UserId { get; set; }
            }

            public class ProjectDeleteContributor
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid UserId { get; set; }
            }

            public class ProjectLeave
            {
                [Required]
                public Guid Id { get; set; }
            }

            public class ProjectChangeTicketStatus
            {
                [Required]
                public Guid Id { get; set; }
            }

            public class ProjectAddTicket
            {
                [Required]
                public string Token { get; set; }

                [Required]
                public string Title { get; set; }

                [Required]
                public string Content { get; set; }
            }

            public class ProjectAddTable
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public string Title { get; set; }
            }

            public class ProjectUpdateTable
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }

                [Required]
                public string Title { get; set;}

                [Required]
                public string Color { get; set; }
            }

            public class ProjectDeleteTable
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }
            }

            public class ProjectAddCard
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }

                [Required]
                public string Title { get; set; }
            }

            public class ProjectUpdateCard
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid CardId { get; set; }

                [Required]
                public string Title { get; set; }

                [Required]
                public string Content { get; set; }

                [Required]
                public string Color { get; set; }

                [Required]
                public DateTime StartingDate { get; set; }

                [Required]
                public bool Notify { get; set; }

                public DateTime? EndingDate { get; set; }

                public Guid? AssignedUserId { get; set; }

                [Required]
                public CardPriorityType Priority { get; set; }
            }

            public class ProjectDeleteCard
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid CardId { get; set; }
            }

            public class ProjectChangeCardStatus
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid CardId { get; set; }
            }

            public class ProjectChangeCardsOrdinalNumber
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public IDictionary<Guid, int> CardOrderMap { get; set; }
            }

            public class ProjectAddComment
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid CardId { get; set; }

                [Required]
                public string Content { get; set; }
            }

            public class ProjectDeleteComment
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid CardId { get; set; }
            }
        }
    }
}
