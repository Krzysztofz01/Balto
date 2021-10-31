using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Projects.ProjectTables
{
    public class ProjectTableTitle : ValueObject<ProjectTableTitle>
    {
        private const int _maxLength = 30;

        public string Value { get; private set; }

        private ProjectTableTitle() { }
        private ProjectTableTitle(string value)
        {
            if (value.IsEmpty() || !value.IsLengthLess(_maxLength))
                throw new ValueObjectValidationException("Invalid project table title length.");

            Value = value;
        }

        public static implicit operator string(ProjectTableTitle title) => title.Value;

        public static ProjectTableTitle FromString(string title) => new ProjectTableTitle(title);
    }
}
