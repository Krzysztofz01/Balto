using Balto.Domain.Shared;

namespace Balto.Domain.Aggregates.Project.Table
{
    public class ProjectTableColor : Color
    {
        protected ProjectTableColor() { }
        protected ProjectTableColor(string value) : base(value) { }

        public static implicit operator string(ProjectTableColor color) => color.Value;

        public static new ProjectTableColor Default => new ProjectTableColor(_defaultValue);
        public static new ProjectTableColor Set(string value) => new ProjectTableColor(value);
    }
}
