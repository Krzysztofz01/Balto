using Newtonsoft.Json;

namespace Balto.Service.Integration.Trello.Models
{
    class Action
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }
}
