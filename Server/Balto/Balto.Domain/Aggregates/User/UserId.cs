using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.User
{
    public class UserId : Value<UserId>
    {
        public Guid Value { get; internal set; }

        protected UserId() { }

        public UserId(Guid value)
        {
            if (value == default) throw new ArgumentNullException(nameof(value), "User id can not be empty.");

            Value = value;
        }

        public static implicit operator Guid(UserId self) => self.Value;
        public static implicit operator UserId(string value) => new UserId(Guid.Parse(value));

        public override string ToString() => Value.ToString();
    }
}
