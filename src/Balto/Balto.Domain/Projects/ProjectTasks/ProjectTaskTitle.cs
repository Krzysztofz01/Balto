using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTaskTitle : ValueObject<ProjectTaskTitle>
    {
        private const int _maxLength = 30;

        public string Value { get; private set; }

        private ProjectTaskTitle() { }
        private ProjectTaskTitle(string value)
        {
            if (value.IsEmpty() || !value.IsLengthLess(_maxLength))
                throw new ValueObjectValidationException("Invalid project task title length.");

            Value = value;
        }

        public static implicit operator string(ProjectTaskTitle title) => title.Value;

        public static ProjectTaskTitle FromString(string title) => new ProjectTaskTitle(title);
    }
}
