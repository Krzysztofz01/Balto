using Balto.Web.Validation;
using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class NotePostView
    {
        [Required]
        [AntiCrossSiteScripting]
        public string Name { get; set; }

        public string Content { get; set; }
    }
}
