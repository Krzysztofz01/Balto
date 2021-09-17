using System.Text.Json.Serialization;

namespace Balto.Application.Integrations.Trello.Models
{
    public class Data
    {
        [JsonPropertyName("card")]
        public Card Card { get; set; }

        [JsonPropertyName("list")]
        public ListInfo List { get; set; }
    }
}
