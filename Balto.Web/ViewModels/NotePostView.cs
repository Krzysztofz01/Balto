using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class NotePostView
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s!@#$%^&*]{1,64}$", ErrorMessage = "Input validation error!")]
        public string Name { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
