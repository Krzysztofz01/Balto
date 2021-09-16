using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Team
{
    public class TeamId : Value<TeamId>
    {
        protected Guid Value { get; set; }

        protected TeamId() { }
        public TeamId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Team id can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(TeamId self) => self.Value;
        public static implicit operator TeamId(string value) => new TeamId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
