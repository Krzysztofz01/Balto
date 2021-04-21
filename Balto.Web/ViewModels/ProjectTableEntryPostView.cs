using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class ProjectTableEntryPostView
    {
        [Required]
        public string Name { get; set; }
        public string Content { get; set; }
    }
}
