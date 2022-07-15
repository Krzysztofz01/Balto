using Balto.Domain.Shared;

namespace Balto.Domain.Team
{
    public class TeamColor : Color
    {
        private TeamColor() : base() { }
        private TeamColor(string value) : base(value) { }
        private TeamColor(System.Drawing.Color color) : base(color) { }

        public static implicit operator string(TeamColor color) => color.Value;
        public static implicit operator System.Drawing.Color(TeamColor color) => color.ToColor();

        public static TeamColor Default => new TeamColor(_defaultValue);
        public static TeamColor FromString(string color) => new TeamColor(color);
        public static TeamColor FromColor(System.Drawing.Color color) => new TeamColor(color);
    }
}
