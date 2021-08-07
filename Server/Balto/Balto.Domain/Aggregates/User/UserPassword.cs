using Balto.Domain.Common;
using Balto.Domain.Extensions;
using System;

namespace Balto.Domain.Aggregates.User
{
    public class UserPassword : Value<UserPassword>
    {
        public string Value { get; private set; }

        protected UserPassword() { }
        protected UserPassword(string value)
        {
            if (value.IsEmpty()) throw new ArgumentException(nameof(value), "Password hash can not be empty.");

            Value = value;
        }

        public static implicit operator string(UserPassword userPassword) => userPassword.Value;

        public static UserPassword FromHash(string value) => new UserPassword(value);
    }
}
