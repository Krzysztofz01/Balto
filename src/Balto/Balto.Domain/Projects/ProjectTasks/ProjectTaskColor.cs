using Balto.Domain.Shared;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTaskColor : Color
    {
        private ProjectTaskColor() : base() { }
        private ProjectTaskColor(string value) : base(value) { }
        private ProjectTaskColor(System.Drawing.Color color) : base(color) { }

        public static implicit operator string(ProjectTaskColor color) => color.Value;
        public static implicit operator System.Drawing.Color(ProjectTaskColor color) => color.ToColor();

        public static ProjectTaskColor Default => new ProjectTaskColor(_defaultValue);
        public static ProjectTaskColor FromString(string color) => new ProjectTaskColor(color);
        public static ProjectTaskColor FromColor(System.Drawing.Color color) => new ProjectTaskColor(color);
    }
}
