using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.User
{
    public class UserTeamId : Value<UserTeamId>
    {
        public Guid Value { get; internal set; }

        protected UserTeamId() { }

        public UserTeamId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Team id value can not be empty!");

            Value = value;
        }

        public static implicit operator Guid(UserTeamId self) => self.Value;

        public static UserTeamId NoTeam => new UserTeamId();
    }
}
