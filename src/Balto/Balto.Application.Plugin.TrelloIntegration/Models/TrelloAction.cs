using System.Text.Json.Serialization;

namespace Balto.Application.Plugin.TrelloIntegration.Models
{
    internal class TrelloAction
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("data")]
        public TrelloData Data { get; set; }
    }
}
