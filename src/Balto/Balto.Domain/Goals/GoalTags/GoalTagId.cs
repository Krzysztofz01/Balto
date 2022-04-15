using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Goals.GoalTags
{
    public class GoalTagId : Identifier
    {
        private GoalTagId() { }
        private GoalTagId(Guid value) : base(value) { }

        public static implicit operator string(GoalTagId contributorId) => contributorId.Value.ToString();
        public static implicit operator Guid(GoalTagId contributorId) => contributorId.Value;

        public static GoalTagId FromGuid(Guid guid) => new GoalTagId(guid);
        public static GoalTagId FromString(string guid) => new GoalTagId(Guid.Parse(guid));
    }
}
