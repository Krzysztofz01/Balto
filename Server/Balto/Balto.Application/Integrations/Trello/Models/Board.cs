using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Balto.Application.Integrations.Trello.Models
{
    public class Board
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("actions")]
        public IEnumerable<Action> Actions { get; set; }

        [JsonPropertyName("lists")]
        public IEnumerable<List> Lists { get; set; }
    }
}
