using System.Collections.Generic;

namespace Balto.Domain
{
    public class Project : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<ProjectTable> Tabels { get; set; }
        public long? OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<ProjectReadWriteUser> ReadWriteUsers { get; set; }
        public virtual ICollection<ProjectReadOnlyUser> ReadOnlyUsers { get; set; }
    }
}
