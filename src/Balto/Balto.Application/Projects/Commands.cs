using Balto.Application.Abstraction;
using Balto.Domain.Projects;
using Balto.Domain.Projects.ProjectContributors;
using Balto.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Balto.Application.Projects
{
    public static class Commands
    {
        public static class V1
        {
            public class Create : IApplicationCommand<Project>
            {
                public string Title { get; set; }
            }

            public class Update : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public string Title { get; set; }

                public bool? TicketStatus { get; set; }
            }

            public class Delete : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }
            }

            public class PushTicket : IApplicationCommand<Project>
            {
                public string TicketToken { get; set; }

                public string Title { get; set; }

                public string Content { get; set; }
            }

            public class AddContributor : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid UserId { get; set; }
            }

            public class DeleteContributor : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid UserId { get; set; }
            }

            public class UpdateContributor : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid UserId { get; set; }

                public ContributorRole? Role { get; set; }
            }

            public class LeaveAsContributor : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }
            }

            public class AddTable : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public string Title { get; set; }
            }

            public class UpdateTable : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid TableId { get; set; }

                public string Title { get; set; }

                public string Color { get; set; }
            }

            public class DeleteTable : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid TableId { get; set; }
            }

            public class TableOrdinalNumbersChanged : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid TableId { get; set; }

                public IEnumerable<Tuple<Guid, int>> IdOrdinalNumberPairs { get; set; }
            }

            public class AddTask : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid TableId { get; set; }

                public string Title { get; set; }
            }

            public class UpdateTask : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid TableId { get; set; }

                public Guid TaskId { get; set; }

                public string Title { get; set; }

                public string Content { get; set; }

                public Guid? AssignedContributorId { get; set; }

                public DateTime? StartingDate { get; set; }

                public DateTime? Deadline { get; set; }

                public PriorityTypes? Priority { get; set; }
            }

            public class DeleteTask : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid TableId { get; set; }

                public Guid TaskId { get; set; }
            }

            public class ChangeTaskStatus : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid TableId { get; set; }

                public Guid TaskId { get; set; }

                public bool? Status { get; set; }
            }

            public class TaskTagAssign : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid TableId { get; set; }

                public Guid TaskId { get; set; }

                public Guid TagId { get; set; }
            }

            public class TaskTagUnassign : IApplicationCommand<Project>
            {
                public Guid Id { get; set; }

                public Guid TableId { get; set; }

                public Guid TaskId { get; set; }

                public Guid TagId { get; set; }
            }
        }
    }
}
