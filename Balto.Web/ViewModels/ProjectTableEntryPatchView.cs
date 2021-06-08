using Balto.Web.Validation;

namespace Balto.Web.ViewModels
{
    public class ProjectTableEntryPatchView
    {
        [AntiCrossSiteScripting]
        public string Name { get; set; }

        [AntiCrossSiteScripting]
        public string Content { get; set; }

        public int Priority { get; set; }

        public string Color { get; set; }
    }
}
