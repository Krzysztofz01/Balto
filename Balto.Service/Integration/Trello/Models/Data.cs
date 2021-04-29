using Newtonsoft.Json;

namespace Balto.Service.Integration.Trello.Models
{
    class Data
    {
        [JsonProperty("card")]
        public Card Card { get; set; }

        [JsonProperty("list")]
        public ListInfo List { get; set; }
    }
}
