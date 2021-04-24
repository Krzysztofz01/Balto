using System;

namespace Balto.Domain
{
    public class Objective : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool Finished { get; set; }
        public bool Daily { get; set; }
        public long? UserId { get; set; }
        public virtual User User { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
    }
}
