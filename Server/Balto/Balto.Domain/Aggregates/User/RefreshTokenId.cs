using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.User
{
    public class RefreshTokenId : Value<RefreshTokenId>
    {
        public Guid Value { get; internal set; }

        protected RefreshTokenId() { }
        public RefreshTokenId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "Refresh token id can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(RefreshTokenId self) => self.Value;
        public static implicit operator RefreshTokenId(string value) => new RefreshTokenId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
