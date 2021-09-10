using Balto.Domain.Shared;

namespace Balto.Domain.Aggregates.Project.Card
{
    public class ProjectTableCardColor : Color
    {
        protected ProjectTableCardColor() { }
        protected ProjectTableCardColor(string value) : base(value) { }

        public static implicit operator string(ProjectTableCardColor color) => color.Value;

        public static new ProjectTableCardColor Default => new ProjectTableCardColor(_defaultValue);
        public static new ProjectTableCardColor Set(string value) => new ProjectTableCardColor(value);
    }
}
