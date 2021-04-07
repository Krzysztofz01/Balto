using System.Collections.Generic;

namespace Balto.Domain
{
    public class Note : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public long? OwnerId { get; set; }
        public virtual User Owner { get; set; }
        public virtual ICollection<NoteReadWriteUser> ReadWriteUsers { get; set; }
        public virtual ICollection<NoteReadOnlyUser> ReadOnlyUsers { get; set; }
    }
}
