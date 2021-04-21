using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class ProjectTablePostView
    {
        [Required]
        public string Name { get; set; }
    }
}
