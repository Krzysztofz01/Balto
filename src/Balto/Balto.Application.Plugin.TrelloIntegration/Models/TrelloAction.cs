using Balto.Application.Plugin.TrelloIntegration.Abstraction;
using System.Text.Json.Serialization;

namespace Balto.Application.Plugin.TrelloIntegration.Models
{
    internal class TrelloAction : IValidableModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("data")]
        public TrelloData Data { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Id)) return false;

            if (Data is null) return false;

            return true;
        }
    }
}
