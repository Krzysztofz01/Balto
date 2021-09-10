using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardFinished : Value<ProjectTableCardFinished>
    {
        public bool Finished { get; private set; }
        public Guid? FinishedBy { get; private set; }
        public DateTime? FinishedOn { get; private set; }

        protected ProjectTableCardFinished() { }
        protected ProjectTableCardFinished(bool status, Guid? userId = null)
        {
            if (status)
            {
                Finished = status;

                if (userId is null) throw new ArgumentNullException(nameof(userId), "You must provide a user id to mark a project card as finished.");
                FinishedBy = userId.Value;

                FinishedOn = DateTime.Now;
            }
            else
            {
                Finished = status;
                FinishedBy = null;
                FinishedOn = null;
            }
        }

        public static implicit operator bool(ProjectTableCardFinished status) => status.Finished;
        public static implicit operator DateTime?(ProjectTableCardFinished status) => status.FinishedOn;

        public static ProjectTableCardFinished Set(Guid userId) => new ProjectTableCardFinished(true, userId);
        public static ProjectTableCardFinished Unfinished => new ProjectTableCardFinished(false);
    }
}
