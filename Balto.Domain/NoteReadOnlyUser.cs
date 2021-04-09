namespace Balto.Domain
{
    public class NoteReadOnlyUser : BaseEntity
    {
        public long? NoteId { get; set; }
        public Note Note { get; set; }
        public long? UserId { get; set; }
        public User User { get; set; }
    }
}
