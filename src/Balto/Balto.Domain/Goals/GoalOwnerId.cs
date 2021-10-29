using Balto.Domain.Shared;
using System;

namespace Balto.Domain.Goals
{
    public class GoalOwnerId : Identifier
    {
        private GoalOwnerId(): base() { }
        private GoalOwnerId(Guid value) : base(value) { }

        public static implicit operator string(GoalOwnerId ownerId) => ownerId.Value.ToString();
        public static implicit operator Guid(GoalOwnerId ownerId) => ownerId.Value;

        public static GoalOwnerId FromString(string guid) => new GoalOwnerId(Guid.Parse(guid));
        public static GoalOwnerId FromGuid(Guid guid) => new GoalOwnerId(guid);
    }
}
