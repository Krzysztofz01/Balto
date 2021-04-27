using System.Collections.Generic;

namespace Balto.Service.Dto
{
    public class NoteDto
    {
        public long Id { get; set; }
        public long OwnerId { get; set; }
        public string OwnerEmail { get; set; }
        public IEnumerable<string> ReadWriteUsersEmails { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
