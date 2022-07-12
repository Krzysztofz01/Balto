using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Balto.Application.Plugin.TrelloIntegration.Models
{
    internal class TrelloTable
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("actions")]
        public IEnumerable<TrelloAction> Actions { get; set; }

        [JsonPropertyName("lists")]
        public IEnumerable<TrelloList> Lists { get; set; }

        [JsonPropertyName("labels")]
        public IEnumerable<TrelloLabel> Labels { get; set; }

        public TrelloTable()
        {
            Actions = new List<TrelloAction>();
            Lists = new List<TrelloList>();
            Labels = new List<TrelloLabel>();
        }
    }
}
