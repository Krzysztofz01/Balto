using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.Team
{
    public class TeamName : Value<TeamName>
    {
        public string Value { get; private set; }

        protected TeamName() { }
        protected TeamName(string value)
        {
            Validate(value);

            Value = value;
        }

        private static void Validate(string value)
        {
            if (value.Length > 25) throw new ArgumentOutOfRangeException(nameof(value), "Team name can not be longer than 25 characters.");
        }

        public static implicit operator string(TeamName title) => title.Value;

        public static TeamName FromString(string value) => new TeamName(value);
    }
}
