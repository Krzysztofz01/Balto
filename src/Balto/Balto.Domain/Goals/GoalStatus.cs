using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Goals
{
    public class GoalStatus : ValueObject<GoalStatus>
    {
        public bool Finished { get; private set; }
        public DateTime? FinishDate { get; private set; }

        private GoalStatus() { }
        private GoalStatus(bool value)
        {
            Finished = value;
            FinishDate = value ? DateTime.Now : null;
        }

        public static implicit operator DateTime?(GoalStatus status) => status.FinishDate;
        public static implicit operator bool(GoalStatus status) => status.Finished;

        public static GoalStatus FromBool(bool status) => new GoalStatus(status);
    }
}
