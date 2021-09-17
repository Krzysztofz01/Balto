using System;
using System.Text.Json.Serialization;

namespace Balto.Application.Integrations.Trello.Models
{
    public class Action
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("data")]
        public Data Data { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
