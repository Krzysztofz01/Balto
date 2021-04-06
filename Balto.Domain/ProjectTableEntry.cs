namespace Balto.Domain
{
    public class ProjectTableEntry : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public long Order { get; set; }
        public bool Finished { get; set; }
        public long? ProjectTableId { get; set; }
        public virtual ProjectTable ProjectTable { get; set; }
    }
}
