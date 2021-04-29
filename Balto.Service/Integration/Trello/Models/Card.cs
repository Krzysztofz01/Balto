using Newtonsoft.Json;

namespace Balto.Service.Integration.Trello.Models
{
    class Card
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("desc")]
        public string Description { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }
    }
}
