using Balto.Domain.Projects.ProjectContributors;
using Balto.Domain.Shared;
using System;
using System.Collections.Generic;

namespace Balto.Application.Projects
{
    public class Dto
    {
        public class ProjectSimple
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
            public string Title { get; set; }
        }

        public class ProjectDetails
        {
            public Guid Id { get; set; }
            public Guid OwnerId { get; set; }
            public string Title { get; set; }
            public string TicketToken { get; set; }
            public IEnumerable<ContributorDetails> Contributors { get; set; }
            public IEnumerable<TableSimple> Tables { get; set; }
        }

        public class ContributorDetails
        {
            public Guid Id { get; set; }
            public ContributorRole Role { get; set; }
        }

        public class TableSimple
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Color { get; set; }
        }

        public class TableDetails
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Color { get; set; }
            public IEnumerable<TaskSimple> Tasks { get; set; }
        }

        public class TaskSimple
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Color { get; set; }
            public Guid? AssignedContributorId { get; set; }
            public DateTime? Deadline { get; set; }
            public bool Finished { get; set; }
            public PriorityTypes Priority { get; set; }
            public int OrdinalNumber { get; set; }
        }

        public class TaskDetails
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
            public string Color { get; set; }
            public Guid CreatorId { get; set; }
            public Guid? AssignedContributorId { get; set; }
            public DateTime StartingDate { get; set; }
            public DateTime? Deadline { get; set; }
            public bool Finished { get; set; }
            public DateTime? FinishDate { get; set; }
            public Guid? FinishedBy { get; set; }
            public PriorityTypes Priority { get; set; }
            public int OrdinalNumber { get; set; }
        }
    }
}
