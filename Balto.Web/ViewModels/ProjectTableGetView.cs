using System.Collections.Generic;

namespace Balto.Web.ViewModels
{
    public class ProjectTableGetView
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<ProjectTableEntryGetView> Entries { get; set; }
    }
}
