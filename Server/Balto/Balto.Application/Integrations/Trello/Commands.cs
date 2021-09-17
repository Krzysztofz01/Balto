using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Balto.Application.Integrations.Trello
{
    public static class Commands
    {
        public static class V1
        {
            public class TrelloBoardAdd
            {
                [Required]
                public IFormFile JsonFile { get; set; }
            }
        }
    }
}
