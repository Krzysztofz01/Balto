using Newtonsoft.Json;
using System.Collections.Generic;

namespace Balto.Service.Integration.Trello.Models
{
    class Table
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("actions")]
        public IEnumerable<Action> Actions { get; set; }

        [JsonProperty("lists")]
        public IEnumerable<List> Lists { get; set; }
    }
}
