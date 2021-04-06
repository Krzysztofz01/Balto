using System.Collections.Generic;

namespace Balto.Domain
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ProjectTable> Tabels { get; set; }
        public long? OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<User> ReadWriteUsers { get; set; }
        public virtual ICollection<User> ReadOnlyUsers { get; set; }
    }
}
