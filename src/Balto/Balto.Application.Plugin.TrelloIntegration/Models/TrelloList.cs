using Balto.Application.Plugin.TrelloIntegration.Abstraction;
using System.Text.Json.Serialization;

namespace Balto.Application.Plugin.TrelloIntegration.Models
{
    internal class TrelloList : IValidableModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("closed")]
        public bool Closed { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Id)) return false;

            if (string.IsNullOrEmpty(Name.Trim())) return false;

            return true;
        }
    }
}
