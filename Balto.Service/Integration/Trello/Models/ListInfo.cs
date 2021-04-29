using Newtonsoft.Json;

namespace Balto.Service.Integration.Trello.Models
{
    class ListInfo
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
