using Balto.Application.Abstraction;
using Balto.Domain.Projects;
using Balto.Domain.Projects.ProjectContributors;
using Balto.Domain.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Projects
{
    public static class Commands
    {
        public static class V1
        {
            public class Create : IApplicationCommand<Project>
            {
                [Required]
                public string Title { get; set; }
            }

            public class Update : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public string Title { get; set; }

                public bool? TicketStatus { get; set; }
            }

            public class Delete : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }
            }

            public class PushTicket : IApplicationCommand<Project>
            {
                [Required]
                public string TicketToken { get; set; }

                [Required]
                public string Title { get; set; }

                [Required]
                public string Content { get; set; }
            }

            public class AddContributor : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid UserId { get; set; }
            }

            public class DeleteContributor : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid UserId { get; set; }
            }

            public class UpdateContributor : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid UserId { get; set; }

                [Required]
                public ContributorRole Role { get; set; }
            }

            public class LeaveAsContributor : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }
            }

            public class AddTable : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public string Title { get; set; }
            }

            public class UpdateTable : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }

                [Required]
                public string Title { get; set; }

                [Required]
                public string Color { get; set; }
            }

            public class DeleteTable : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }
            }

            public class TableOrdinalNumbersChanged : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }

                [Required]
                public IEnumerable<Tuple<Guid, int>> IdOrdinalNumberPairs { get; set; }
            }

            public class AddTask : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }

                [Required]
                public string Title { get; set; }
            }

            public class UpdateTask : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }

                [Required]
                public Guid TaskId { get; set; }

                [Required]
                public string Title { get; set; }

                public string Content { get; set; }

                public Guid? AssignedContributorId { get; set; }

                [Required]
                public DateTime StartingDate { get; set; }

                public DateTime? Deadline { get; set; }

                [Required]
                public PriorityTypes Priority { get; set; }
            }

            public class DeleteTask : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }

                [Required]
                public Guid TaskId { get; set; }
            }

            public class ChangeTaskStatus : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }

                [Required]
                public Guid TaskId { get; set; }

                [Required]
                public bool Status { get; set; }
            }

            public class TaskTagAssign : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }

                [Required]
                public Guid TaskId { get; set; }

                [Required]
                public Guid TagId { get; set; }
            }

            public class TaskTagUnassign : IApplicationCommand<Project>
            {
                [Required]
                public Guid Id { get; set; }

                [Required]
                public Guid TableId { get; set; }

                [Required]
                public Guid TaskId { get; set; }

                [Required]
                public Guid TagId { get; set; }
            }
        }
    }
}
