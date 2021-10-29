using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Identities
{
    public class IdentityName : ValueObject<IdentityName>
    {
        private const int _maxNameLength = 40;

        public string Value { get; private set; }

        private IdentityName() { }
        private IdentityName(string value)
        {
            if (value.IsEmpty() || !value.IsLengthLess(_maxNameLength))
                throw new ValueObjectValidationException("The identity name length is invalid.");

            Value = value;
        }

        public static implicit operator string(IdentityName name) => name.Value;

        public static IdentityName FromString(string name) => new IdentityName(name);
    }
}
