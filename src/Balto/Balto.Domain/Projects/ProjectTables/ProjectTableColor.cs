using Balto.Domain.Shared;

namespace Balto.Domain.Projects.ProjectTables
{
    public class ProjectTableColor : Color
    {
        private ProjectTableColor() : base() { }
        private ProjectTableColor(string value) : base(value) { }
        private ProjectTableColor(System.Drawing.Color color) : base(color) { }

        public static implicit operator string(ProjectTableColor color) => color.Value;
        public static implicit operator System.Drawing.Color(ProjectTableColor color) => color.ToColor();

        public static ProjectTableColor Default => new ProjectTableColor(_defaultValue);
        public static ProjectTableColor FromString(string color) => new ProjectTableColor(color);
        public static ProjectTableColor FromColor(System.Drawing.Color color) => new ProjectTableColor(color);
    }
}
