using Balto.Web.Validation;

namespace Balto.Web.ViewModels
{
    public class TeamPatchView
    {
        [AntiCrossSiteScripting]
        public string Name { get; set; }
    }
}
