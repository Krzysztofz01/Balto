using System.Text.Json.Serialization;

namespace Balto.Application.Integrations.Trello.Models
{
    public class ListInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
