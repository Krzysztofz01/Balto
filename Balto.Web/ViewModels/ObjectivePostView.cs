using System;
using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class ObjectivePostView
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s!@#$%^&*]{1,64}$", ErrorMessage = "Input validation error!")]
        public string Name { get; set; }

        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s!@#$%^&*]{1,64}$", ErrorMessage = "Input validation error!")]
        public string Description { get; set; }
        
        [Required]
        public DateTime? StartingDate { get; set; }
        
        [Required]
        public DateTime? EndingDate { get; set; }
    }
}
