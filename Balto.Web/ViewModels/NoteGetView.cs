using System.Collections.Generic;

namespace Balto.Web.ViewModels
{
    public class NoteGetView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public string OwnerEmail { get; set; }
        public IEnumerable<string> ReadWriteUsersEmails { get; set; }
    }
}
