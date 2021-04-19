using System;
using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class ObjectivePostView
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }
        
        [Required]
        public DateTime StartingDate { get; set; }
        
        [Required]
        public DateTime EndingDate { get; set; }
    }
}
