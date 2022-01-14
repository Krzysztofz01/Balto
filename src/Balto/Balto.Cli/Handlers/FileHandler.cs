using Balto.Cli.Client;
using System.IO;
using System.Threading.Tasks;

namespace Balto.Cli.Handlers
{
    public static class FileHandler
    {
        private const string _clientConfigurationFileName = "config.json";

        public static bool CheckClientConfigurationFile()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _clientConfigurationFileName);

            return File.Exists(filePath);
        }

        public static ClientConfiguration GetClientConfiguration()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), _clientConfigurationFileName);

            var serializedClientConfiguration = File.ReadAllText(filePath);

            return ClientConfiguration.Factory.Deserialize(serializedClientConfiguration);
        }

        public static async Task<bool> SaveClientConfigurationAsync(ClientConfiguration clientConfiguration)
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), _clientConfigurationFileName);

                var serializedClientConfiguration = clientConfiguration.Serialize();

                await File.WriteAllTextAsync(filePath, serializedClientConfiguration);

                return true;
            }
            catch
            {
                return false;
                throw;
            }
        }
    }
}
