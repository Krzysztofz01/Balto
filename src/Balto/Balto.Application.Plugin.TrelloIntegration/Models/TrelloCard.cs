using Balto.Application.Plugin.TrelloIntegration.Abstraction;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Balto.Application.Plugin.TrelloIntegration.Models
{
    internal class TrelloCard : IValidableModel
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("desc")]
        public string Description { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("closed")]
        public bool Closed { get; set; }

        [JsonPropertyName("labels")]
        public IEnumerable<TrelloLabel> Labels { get; set; }

        public TrelloCard()
        {
            Labels = new List<TrelloLabel>();
        }

        public bool IsValid()
        {
            //TODO: Implement vaidation if needed
            return true;
        }
    }
}
