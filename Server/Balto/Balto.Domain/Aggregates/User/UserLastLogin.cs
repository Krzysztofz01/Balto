using Balto.Domain.Common;
using System;

namespace Balto.Domain.Aggregates.User
{
    public class UserLastLogin : Value<UserLastLogin>
    {
        public string IpAddress { get; private set; }
        public DateTime Date { get; private set; }

        protected UserLastLogin() { }
        protected UserLastLogin(string ipAddress)
        {
            IpAddress = ipAddress;
            Date = DateTime.Now;
        }

        public static implicit operator string(UserLastLogin lastLogin) => lastLogin.IpAddress;
        public static implicit operator DateTime(UserLastLogin lastLogin) => lastLogin.Date;

        public static UserLastLogin Set(string ipAddress) => new UserLastLogin(ipAddress);
    }
}
