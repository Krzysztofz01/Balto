using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Projects.ProjectTasks
{
    public class ProjectTaskOrdinalNumber : ValueObject<ProjectTaskOrdinalNumber>
    {
        private const int _minimalValue = 1;

        public int Value { get; private set; }

        private ProjectTaskOrdinalNumber() { }
        private ProjectTaskOrdinalNumber(int value)
        {
            if (value < _minimalValue)
                throw new ValueObjectValidationException("The project task ordinal value can not be less than the declared minimum");

            Value = value;
        }

        public static implicit operator int(ProjectTaskOrdinalNumber ordinalNumber) => ordinalNumber.Value;

        public static ProjectTaskOrdinalNumber Default => new ProjectTaskOrdinalNumber(_minimalValue);
        public static ProjectTaskOrdinalNumber FromInt(int ordinalNumber) => new ProjectTaskOrdinalNumber(ordinalNumber);
    }
}
