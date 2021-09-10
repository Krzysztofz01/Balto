using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardDeadline : Value<ProjectTableCardDeadline>
    {
        public DateTime? Date { get; private set; }
        public Guid? UserId { get; private set; }
        public bool Notify { get; private set; }

        protected ProjectTableCardDeadline() { }
        protected ProjectTableCardDeadline(bool notify, DateTime? date=null, Guid? userId = null)
        {
            if (date != null)
            {
                Validate(date.Value);
            }

            Notify = notify;
            UserId = userId;
            Date = date;
        }

        private static void Validate(DateTime value)
        {
            if (value == default) throw new ArgumentException(nameof(value), "You can not use the default date value.");

            if (value < DateTime.Now) throw new ArgumentException(nameof(value), "You can not set the deadline of a project task in the past.");
        }

        public static implicit operator string(ProjectTableCardDeadline deadline) => deadline.Date.ToString();
        public static implicit operator DateTime?(ProjectTableCardDeadline deadline) => deadline.Date;
        public static implicit operator bool(ProjectTableCardDeadline deadline) => deadline.Notify;

        public static ProjectTableCardDeadline Set(bool notify, DateTime? date = null, Guid? userId = null) => new ProjectTableCardDeadline(notify, date, userId);
        public static ProjectTableCardDeadline Default => new ProjectTableCardDeadline(false, null);
    }
}
