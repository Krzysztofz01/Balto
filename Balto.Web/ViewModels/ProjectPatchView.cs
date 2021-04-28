using Balto.Web.Validation;

namespace Balto.Web.ViewModels
{
    public class ProjectPatchView
    {
        [AntiCrossSiteScripting]
        public string Name { get; set; }
    }
}
