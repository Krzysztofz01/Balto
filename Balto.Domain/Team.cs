using System.Collections.Generic;

namespace Balto.Domain
{
    public class Team : BaseEntity
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
