using Balto.Domain.Common;
using System;
using System.Security.Cryptography;

namespace Balto.Domain.Aggregates.User
{
    public class RefreshToken : Entity<RefreshTokenId>
    {
        //Presistance
        public Guid RefreshTokenId { get; private set; }


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
        public RefreshToken(Action<object> applier) : base(applier) { }


        //Methods
        public bool IsExpired => DateTime.Now >= Expires;

        public bool IsActive => Revoked == null && !IsExpired;


        //Entity abstraction implementation
        protected override void When(object @event)
        {
            switch(@event)
            {
                case Events.UserAuthenticated e:
                    InitializeRefreshToken(e.IpAddress);
                    break;

                case Events.UserTokenRefreshed e:
                    InitializeRefreshToken(e.IpAddress);
                    break;

                case Events.UserTokenRevoked e:
                    RevokeRefreshToken(e.IpAddress);
                    break;

                case Events.RefreshTokenReplacedByTokenChanged e:
                    ReplacedByToken = e.Token;
                    break;
            }
        }

        protected void InitializeRefreshToken(string ipAddress)
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            
            var randomBytes = new byte[64];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            Id = new RefreshTokenId(Guid.NewGuid());
            Token = Convert.ToBase64String(randomBytes);
            Expires = DateTime.Now.AddDays(_refershTokenExpirationDays);
            Created = DateTime.Now;
            CreatedByIp = ipAddress;
            IsRevoked = false;
            Revoked = null;
        }

        protected void RevokeRefreshToken(string ipAddress)
        {
            IsRevoked = true;
            Revoked = DateTime.Now;
            RevokedByIp = ipAddress;
        }
    }
}
