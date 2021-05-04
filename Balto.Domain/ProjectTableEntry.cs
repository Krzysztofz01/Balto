namespace Balto.Domain
{
    public class ProjectTableEntry : BaseEntity
    {
        public string Name { get; set; }
        public string Content { get; set; }
        public long Order { get; set; }
        public bool Finished { get; set; }
        public int Priority { get; set; }
        public long? ProjectTableId { get; set; }
        public virtual ProjectTable ProjectTable { get; set; }
        public long? UserAddedId { get; set; }
        public virtual User UserAdded { get; set; }
        public long? UserFinishedId { get; set; }
        public virtual User UserFinished { get; set; }
    }
}
