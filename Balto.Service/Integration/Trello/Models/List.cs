using Newtonsoft.Json;

namespace Balto.Service.Integration.Trello.Models
{
    class List
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("closed")]
        public bool Closed { get; set; }
    }
}
