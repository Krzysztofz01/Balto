using Balto.Domain.Common;
using System;
using System.Security.Cryptography;

namespace Balto.Domain.Aggregates.User
{
    public class RefreshToken : Entity<RefreshTokenId>
    {
        //Presistance
        public Guid RefreshTokenId { get => Id.Value; set { } }


        //Properties
        public string Token { get; private set; }
        public DateTime Expires { get; private set; }
        public DateTime Created { get; private set; }
        public string CreatedByIp { get; private set; }
        public DateTime? Revoked { get; private set; }
        public string RevokedByIp { get; private set; }
        public bool IsRevoked { get; private set; }
        public string ReplacedByToken { get; private set; }

        //Const values
        private const int _refershTokenExpirationDays = 7;
        

        //Constructors
        protected RefreshToken() { }
        protected RefreshToken(string ipAddress)
        {
            using (var rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                var randomBytes = new byte[64];
                rngCryptoServiceProvider.GetBytes(randomBytes);

                Token = Convert.ToBase64String(randomBytes);
                Expires = DateTime.Now.AddDays(_refershTokenExpirationDays);
                Created = DateTime.Now;
                CreatedByIp = ipAddress;
                IsRevoked = false;
                Revoked = null;
            }
        }


        //Methods
        public bool IsExpired => DateTime.Now >= Expires;

        public bool IsActive => Revoked == null && !IsExpired;

        public void Revoke(string ipAddress, string token = null) =>
            Apply(new Events.RefreshTokenRevoke
            {
                IpAddress = ipAddress,
                Token = token
            });
        

        //Entity abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.RefreshTokenRevoke e:
                    IsRevoked = true;
                    Revoked = DateTime.Now;
                    RevokedByIp = e.IpAddress;
                    ReplacedByToken = e.Token;
                    break;
            }
        }

        //Factory
        public static class Factory
        {
            public static RefreshToken Create(string ipAddress)
            {
                return new RefreshToken(ipAddress);
            }
        }
    }
}
