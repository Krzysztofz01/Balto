using Balto.Domain.Shared;

namespace Balto.Domain.Goals
{
    public class GoalColor : Color
    {
        private GoalColor() : base() { }
        private GoalColor(string value) : base(value) { }
        private GoalColor(System.Drawing.Color color) : base(color) { }

        public static implicit operator string(GoalColor color) => color.Value;
        public static implicit operator System.Drawing.Color(GoalColor color) => color.ToColor();

        public static GoalColor Default => new GoalColor(_defaultValue);
        public static GoalColor FromString(string color) => new GoalColor(color);
        public static GoalColor FromColor(System.Drawing.Color color) => new GoalColor(color);
    }
}
