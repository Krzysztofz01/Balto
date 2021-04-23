using Balto.Web.Validation;
using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class ProjectTableEntryPostView
    {
        [Required]
        [AntiCrossSiteScripting]
        public string Name { get; set; }
        
        [AntiCrossSiteScripting]
        public string Content { get; set; }
    }
}
