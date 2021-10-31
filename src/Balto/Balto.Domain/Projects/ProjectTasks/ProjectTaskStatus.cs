using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTaskStatus : ValueObject<ProjectTaskStatus>
    {
        public bool Finished { get; private set; }
        public DateTime? FinishDate { get; private set; }
        public Guid? FinishedBy { get; private set; }

        private ProjectTaskStatus() { }
        private ProjectTaskStatus(Guid? finishedBy)
        {
            if (finishedBy.HasValue)
            {
                Finished = true;
                FinishDate = DateTime.Now;
                FinishedBy = finishedBy;
            }
            else
            {
                Finished = false;
                FinishDate = null;
                FinishedBy = null;
            }
        }

        public static implicit operator bool(ProjectTaskStatus taskStatus) => taskStatus.Finished;
        public static implicit operator DateTime?(ProjectTaskStatus taskStatus) => taskStatus.FinishDate;
        public static implicit operator Guid?(ProjectTaskStatus taskStatus) => taskStatus.FinishedBy;

        public static ProjectTaskStatus Unfinished => new ProjectTaskStatus(null);
        public static ProjectTaskStatus FinishedByContributor(Guid finishedBy) => new ProjectTaskStatus(finishedBy);
    }
}
