using System.Text.Json.Serialization;

namespace Balto.Application.Plugin.TrelloIntegration.Models
{
    internal class TrelloData
    {
        [JsonPropertyName("card")]
        public TrelloCard Card { get; set; }

        [JsonPropertyName("list")]
        public TrelloListInfo List { get; set; }
    }
}
