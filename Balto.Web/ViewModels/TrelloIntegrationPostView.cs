using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Balto.Web.ViewModels
{
    public class TrelloIntegrationPostView
    {
        [Required]
        public IFormFile jsonFile { get; set; }
    }
}
