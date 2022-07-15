using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Team
{
    public class TeamName : ValueObject<TeamName>
    {
        private const int _maxNameLength = 40;

        public string Value { get; private set; }

        private TeamName() { }
        private TeamName(string value)
        {
            if (value.IsEmpty() || !value.IsLengthLess(_maxNameLength))
                throw new ValueObjectValidationException("The identity name length is invalid.");

            Value = value;
        }

        public static implicit operator string(TeamName name) => name.Value;

        public static TeamName FromString(string name) => new TeamName(name);
    }
}
