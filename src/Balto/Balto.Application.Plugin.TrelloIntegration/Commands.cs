using Balto.Application.Plugin.Core;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Plugin.TrelloIntegration
{
    public static class Commands
    {
        public class CreateProjectFromTrelloBoard : IPluginCommand
        {
            [Required]
            public IFormFile JsonBoardFile { get; set; }
        }
    }
}
