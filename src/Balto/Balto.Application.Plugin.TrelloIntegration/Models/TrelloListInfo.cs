using System.Text.Json.Serialization;

namespace Balto.Application.Plugin.TrelloIntegration.Models
{
    internal class TrelloListInfo
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
