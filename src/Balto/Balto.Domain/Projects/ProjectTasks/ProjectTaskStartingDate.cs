using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;
using System;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTaskStartingDate : ValueObject<ProjectTaskStartingDate>
    {
        public DateTime Value { get; private set; }

        private ProjectTaskStartingDate() { }
        private ProjectTaskStartingDate(DateTime value)
        {
            if (value == default)
                throw new ValueObjectValidationException("The project task starting date can not have the default value.");

            Value = value;
        }

        public static implicit operator DateTime(ProjectTaskStartingDate startingDate) => startingDate.Value;

        public static ProjectTaskStartingDate Now => new ProjectTaskStartingDate(DateTime.Now);
        public static ProjectTaskStartingDate FromDateTime(DateTime startingDate) => new ProjectTaskStartingDate(startingDate);
    }
}
