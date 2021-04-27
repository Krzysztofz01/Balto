using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class CollaborationInvitation
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
