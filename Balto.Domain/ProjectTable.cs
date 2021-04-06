using System.Collections.Generic;

namespace Balto.Domain
{
    public class ProjectTable : BaseEntity
    {
        public string Name { get; set; }
        public long? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public virtual ICollection<ProjectTableEntry> Entries { get; set; }
    }
}
