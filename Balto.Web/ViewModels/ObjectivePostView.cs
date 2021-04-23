using Balto.Web.Validation;
using System;
using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class ObjectivePostView
    {
        [Required]
        [AntiCrossSiteScripting]
        public string Name { get; set; }

        [AntiCrossSiteScripting]
        public string Description { get; set; }

        [Required]
        public DateTime? StartingDate { get; set; }

        [Required]
        [ObjectiveDateCompare]
        public DateTime? EndingDate { get; set; }
    }
}
