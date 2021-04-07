namespace Balto.Domain
{
    public class NoteReadOnlyUser
    {
        public long? NoteId { get; set; }
        public Note Note { get; set; }
        public long? UserId { get; set; }
        public User User { get; set; }
    }
}
