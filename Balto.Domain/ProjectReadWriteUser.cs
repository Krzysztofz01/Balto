namespace Balto.Domain
{
    public class ProjectReadWriteUser
    {
        public long? ProjectId { get; set; }
        public virtual Project Project { get; set; }
        public long? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
