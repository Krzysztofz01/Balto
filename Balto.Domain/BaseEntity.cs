using System;

namespace Balto.Domain
{
    public class BaseEntity
    {
        public long Id { get; set; }
        public DateTime AddDate { get; set; }
        public DateTime EditDate { get; set; }
    }
}
