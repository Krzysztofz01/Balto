using System.Text.Json.Serialization;

namespace Balto.Application.Integrations.Trello.Models
{
    public class List
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("closed")]
        public bool Closed { get; set; }
    }
}
