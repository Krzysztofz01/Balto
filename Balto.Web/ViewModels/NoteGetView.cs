using System.Collections.Generic;

namespace Balto.Web.ViewModels
{
    public class NoteGetView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public UserGetView Owner { get; set; }
        public IEnumerable<UserGetView> ReadWriteUsers { get; set; }
    }
}
