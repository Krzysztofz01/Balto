using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class ProjectPostView
    {
        [Required]
        public string Name { get; set; }
    }
}
