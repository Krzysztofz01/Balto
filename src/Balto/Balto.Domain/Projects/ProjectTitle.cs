using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Projects
{
    public class ProjectTitle : ValueObject<ProjectTitle>
    {
        private const int _maxLength = 30;

        public string Value { get; private set; }

        private ProjectTitle() { }
        private ProjectTitle(string value)
        {
            if (value.IsEmpty() || !value.IsLengthLess(_maxLength))
                throw new ValueObjectValidationException("Invalid project title length.");

            Value = value;
        }

        public static implicit operator string(ProjectTitle title) => title.Value;

        public static ProjectTitle FromString(string title) => new ProjectTitle(title);
    }
}
