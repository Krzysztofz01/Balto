using Balto.Application.Plugin.TrelloIntegration.Abstraction;
using System.Text.Json.Serialization;

namespace Balto.Application.Plugin.TrelloIntegration.Models
{
    internal class TrelloListInfo : IValidableModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        public bool IsValid()
        {
            if (string.IsNullOrEmpty(Id)) return false;

            return true;
        }
    }
}
