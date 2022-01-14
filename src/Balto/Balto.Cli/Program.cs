using Balto.Cli.Abstraction;
using Balto.Cli.Client;
using Balto.Cli.Handlers;
using Balto.Cli.Modules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Cli
{
    public class Program
    {
        private static ClientConfiguration Client;
        private static List<IModule> Modules;
        private static IOutput Output;

        private const int _successCode = 0;
        private const int _failureCode = 1;

        private static async Task<int> Main(string[] args)
        {
            await Initialize();

            try
            {
                await RunModule(args);

                return _successCode;
            }
            catch (Exception)
            {
                return _failureCode;
            }
        }

        private static async Task Initialize()
        {
            if (!FileHandler.CheckClientConfigurationFile())
            {
                var client = ClientConfiguration.Factory.Initialize();
                if (!await FileHandler.SaveClientConfigurationAsync(client))
                    throw new InvalidOperationException("Unable to save client configuration");
            }
            
            Client = FileHandler.GetClientConfiguration();

            //TODO: Output implementation

            Modules = new List<IModule>
            {
                new ConfigurationModule(Client)
            };
        }

        private static async Task RunModule(string[] args)
        {
            if (!args.Any())
            {
                //TODO: Print help
            }

            string moduleSelector = args.First().ToLower();

            await Modules.Single(m => m.ModuleName.ToLower() == moduleSelector).Invoke(args.Skip(1).ToArray());
        }
    }
}
