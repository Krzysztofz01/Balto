using System.Text.Json.Serialization;

namespace Balto.Application.Plugin.TrelloIntegration.Models
{
    internal class TrelloLabel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("color")]
        public string ColorName { get; set; }
    }
}
