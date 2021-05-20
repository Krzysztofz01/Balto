using Balto.Web.Validation;
using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class TeamPostView
    {
        [Required]
        [AntiCrossSiteScripting]
        public string Name { get; set; }

        public string Color { get; set; }
    }
}
