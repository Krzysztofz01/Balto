using System;

namespace Balto.Web.ViewModels
{
    public class ProjectTableEntryGetView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Content { get; set; }
        public long Order { get; set; }
        public bool Finished { get; set; }
        public int Priority { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime EndingDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool Notify { get; set; }
        public UserGetView UserAdded { get; set; }
        public UserGetView UserFinished { get; set; }
    }
}
