using Balto.Application.Plugin.TrelloIntegration.Abstraction;
using System.Text.Json.Serialization;

namespace Balto.Application.Plugin.TrelloIntegration.Models
{
    internal class TrelloData : IValidableModel
    {
        [JsonPropertyName("card")]
        public TrelloCard Card { get; set; }

        [JsonPropertyName("list")]
        public TrelloListInfo List { get; set; }

        public bool IsValid()
        {
            if (Card is null) return false;

            if (List is null) return false;

            return true;
        }
    }
}
