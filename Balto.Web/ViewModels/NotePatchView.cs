using Balto.Web.Validation;

namespace Balto.Web.ViewModels
{
    public class NotePatchView
    {
        [AntiCrossSiteScripting]
        public string Name { get; set; }

        [AntiCrossSiteScripting]
        public string Content { get; set; }
    }
}
