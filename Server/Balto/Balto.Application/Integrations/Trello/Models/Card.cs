using System.Text.Json.Serialization;

namespace Balto.Application.Integrations.Trello.Models
{
    public class Card
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("desc")]
        public string Description { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("closed")]
        public bool Closed { get; set; }
    }
}
