using System;

namespace Balto.Domain.Core.Model
{
    public abstract class AuditableObject
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
