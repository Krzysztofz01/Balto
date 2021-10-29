using Balto.Domain.Core.Exceptions;
using Balto.Domain.Core.Extensions;
using Balto.Domain.Core.Model;

namespace Balto.Domain.Identities
{
    public class IdentityPasswordHash : ValueObject<IdentityPasswordHash>
    {
        public string Value { get; private set; }

        private IdentityPasswordHash() { }
        private IdentityPasswordHash(string value)
        {
            if (value.IsEmpty())
                throw new ValueObjectValidationException("The identity password hash length is invalid");

            Value = value;
        }

        public static implicit operator string(IdentityPasswordHash passwordHash) => passwordHash.Value;

        public static IdentityPasswordHash FromString(string passwordHash) => new IdentityPasswordHash(passwordHash);
    }
}
