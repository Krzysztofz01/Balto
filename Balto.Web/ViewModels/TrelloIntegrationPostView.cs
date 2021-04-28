using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class TrelloIntegrationPostView
    {
        [Required]
        IFormFile jsonFile { get; set; }
    }
}
