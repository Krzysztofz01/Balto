using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTaskContent : ValueObject<ProjectTaskContent>
    {
        private const int _maxLength = 300;

        public string Value { get; private set; }

        private ProjectTaskContent() { }
        private ProjectTaskContent(string value)
        {
            if (!value.IsLengthLess(_maxLength))
                throw new ValueObjectValidationException("Invalid project task content length.");

            Value = value;
        }

        public static implicit operator string(ProjectTaskContent description) => description.Value;

        public static ProjectTaskContent FromString(string description) => new ProjectTaskContent(description);
        public static ProjectTaskContent Empty => new ProjectTaskContent(string.Empty);
    }
}
