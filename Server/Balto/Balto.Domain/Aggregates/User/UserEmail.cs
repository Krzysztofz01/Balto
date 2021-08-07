using Balto.Domain.Common;
using System;
using System.Text.RegularExpressions;

namespace Balto.Domain.Aggregates.User
{
    public class UserEmail : Value<UserEmail>
    {
        private const string _emailValidationRegexPattern =
            @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|" + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)" + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        public string Value { get; private set; }

        protected UserEmail() { }
        protected UserEmail(string email)
        {
            Validate(email);
            Value = email;
        }

        private static void Validate(string email)
        {
            var regex = new Regex(_emailValidationRegexPattern, RegexOptions.IgnoreCase);

            if (!regex.IsMatch(email)) throw new ArgumentException(nameof(email), "Invalid email format.");
        }

        public static implicit operator string(UserEmail email) => email.Value;

        public static UserEmail FromString(string value) => new UserEmail(value);
    }
}
