using System.Text.Json.Serialization;

namespace Balto.Application.Plugin.TrelloIntegration.Models
{
    internal class TrelloList
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("closed")]
        public bool Closed { get; set; }
    }
}
