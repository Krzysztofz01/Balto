using System;

namespace Balto.Domain
{
    public class RefreshToken : BaseEntity
    {
        public string Token { get; set; }
        
        public DateTime Expires { get; set; }
        public bool IsExpired => DateTime.UtcNow >= Expires;
        
        public DateTime Created { get; set; }
        public string CreatedByIp { get; set; }
        
        public DateTime? Revoked { get; set; }
        public string RevokedByIp { get; set; }
        public bool IsRevoked { get; set; }
        
        public string ReplacedByToken { get; set; }
        public bool IsActive => Revoked == null && !IsExpired;

        public long UserId { get; set; }
        public User User { get; set; }
    }
}
