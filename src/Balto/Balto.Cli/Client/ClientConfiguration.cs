using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Balto.Cli.Client
{
    public class ClientConfiguration
    {
        [Configurable]
        public string Email { get; set; }

        [Configurable]
        public string Password { get; set; }

        [Configurable]
        public string ServerAddress { get; set; }

        public void ApplyConfiguration(string property, string value)
        {
            var selectedProp = typeof(ClientConfiguration)
                .GetProperties()
                .Where(p => p.CustomAttributes.Any(a => a.AttributeType == typeof(ConfigurableAttribute)))
                .Single(p => p.Name.ToLower() == property.ToLower());

            selectedProp.SetValue(this, value);
        }

        [JsonConstructor]
        public ClientConfiguration() { }

        public string Serialize() =>
            JsonSerializer.Serialize(this);

        public static class Factory
        {
            public static ClientConfiguration Initialize()
            {
                return new ClientConfiguration();
            }

            public static ClientConfiguration Deserialize(string serializedClientConfiguration)
            {
                return JsonSerializer.Deserialize<ClientConfiguration>(serializedClientConfiguration);
            }
        }
    }
}
