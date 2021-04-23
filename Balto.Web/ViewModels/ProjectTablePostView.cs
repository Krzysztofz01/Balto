using Balto.Web.Validation;
using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class ProjectTablePostView
    {
        [Required]
        [AntiCrossSiteScripting]
        public string Name { get; set; }
    }
}
