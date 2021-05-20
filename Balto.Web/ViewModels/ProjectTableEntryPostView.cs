using Balto.Web.Validation;
using System;
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

        public int Priority { get; set; }

        [Required]
        public DateTime StartingDate { get; set; }

        [Required]
        public DateTime EndingDate { get; set; }

        public bool Notify { get; set; }
    }
}
