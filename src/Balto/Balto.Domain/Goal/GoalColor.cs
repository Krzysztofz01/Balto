using Balto.Domain.Shared;

namespace Balto.Domain.Goal
{
    public class GoalColor : Color
    {
        private GoalColor() : base() { }
        private GoalColor(string value) : base(value) { }
        private GoalColor(System.Drawing.Color color) : base(color) { }

        public static implicit operator string(GoalColor color) => color.Value;
        public static implicit operator System.Drawing.Color(GoalColor color) => color.ToColor();

        public GoalColor Default => new GoalColor(_defaultValue);
        public GoalColor FromString(string color) => new GoalColor(color);
        public GoalColor FromColor(System.Drawing.Color color) => new GoalColor(color);
    }
}
