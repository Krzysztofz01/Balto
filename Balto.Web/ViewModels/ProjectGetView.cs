using System.Collections.Generic;

namespace Balto.Web.ViewModels
{
    public class ProjectGetView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string OwnerEmail { get; set; }
        public IEnumerable<string> ReadWriteUsersEmails { get; set; }
        public IEnumerable<ProjectTableGetView> Tabels { get; set; }
    }
}
