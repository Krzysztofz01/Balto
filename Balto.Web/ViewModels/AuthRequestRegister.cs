using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class AuthRequestRegister
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password length can't be less than 8.")]
        public string Password { get; set; }

        [Required]
        [MinLength(8, ErrorMessage = "Password length can't be less than 8.")]
        [Compare(nameof(Password), ErrorMessage = "Passwords dont't match.")]
        public string RepeatPassword { get; set; }
    }
}
