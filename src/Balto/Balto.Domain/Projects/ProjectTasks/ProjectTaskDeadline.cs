using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTaskDeadline : ValueObject<ProjectTaskDeadline>
    {
        public DateTime? Value { get; private set; }

        private ProjectTaskDeadline() { }
        private ProjectTaskDeadline(DateTime? value)
        {
            if (value.HasValue)
            {
                if (value.Value == default)
                    throw new ValueObjectValidationException("The project task deadline date can not have the default value");
            }

            Value = value;
        }

        public static implicit operator DateTime?(ProjectTaskDeadline deadline) => deadline.Value;

        public static ProjectTaskDeadline NoDeadline => new ProjectTaskDeadline(null);
        public static ProjectTaskDeadline FromDateTime(DateTime deadline) => new ProjectTaskDeadline(deadline);
    }
}
