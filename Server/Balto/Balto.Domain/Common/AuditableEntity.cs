using System;

namespace Balto.Domain.Common
{
    public abstract class AuditableEntity
    {
        public DateTime CreatedAt { get; private set; }
        public DateTime ModifedAt { get; private set; }
        public DateTime? DeletedAt { get; private set; }

        public void SetCreateDate() => CreatedAt = DateTime.Now;
        public void SetModifiedDate() => ModifedAt = DateTime.Now;
        protected void SetAsDeleted() => DeletedAt = DateTime.Now;
    }
}
