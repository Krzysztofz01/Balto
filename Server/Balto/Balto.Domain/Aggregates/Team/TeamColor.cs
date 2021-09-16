using Balto.Domain.Shared;

namespace Balto.Domain.Aggregates.Team
{
    public class TeamColor : Color
    {
        protected TeamColor() { }
        protected TeamColor(string value) : base(value) { }

        public static implicit operator string(TeamColor color) => color.Value;

        public static new TeamColor Default => new TeamColor(_defaultValue);
        public static new TeamColor Set(string value) => new TeamColor(value);
    }
}
