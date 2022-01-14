using Balto.Cli.Abstraction;
using Balto.Cli.Client;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Cli.Modules
{
    public class ConfigurationModule : IModule
    {
        private const string _moduleName = "config";
        public string ModuleName => _moduleName;

        private readonly ClientConfiguration _clientConfiguration;

        public ConfigurationModule(ClientConfiguration clientConfiguration)
        {
            _clientConfiguration = clientConfiguration ??
                throw new ArgumentNullException(nameof(clientConfiguration));
        }

        public async Task Invoke(string[] args)
        {
            if (args.Count() < 2)
            {
                //TODO: Print help
            }

            string property = args[0];
            string value = args[1];

            _clientConfiguration.ApplyConfiguration(property, value);
        }
    }
}
