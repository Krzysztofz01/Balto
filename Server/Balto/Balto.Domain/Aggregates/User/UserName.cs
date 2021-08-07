using Balto.Domain.Common;
using Balto.Domain.Extensions;
using System;

namespace Balto.Domain.Aggregates.User
{
    public class UserName : Value<UserName>
    {
        public string Value { get; private set; }

        protected UserName() { }
        protected UserName(string name)
        {
            Validate(name);

            Value = name;
        }

        private static void Validate(string value)
        {
            if (value.IsEmpty()) throw new ArgumentException(nameof(value), "User name can not be empty.");

            if (!value.LengthBetween(0, 35)) throw new ArgumentException(nameof(value), "Invalid user name length.");
        }

        public static implicit operator string(UserName userName) => userName.Value;

        public static UserName FromString(string value) => new UserName(value);
    }
}
