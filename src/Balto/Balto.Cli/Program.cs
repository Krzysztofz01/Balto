using Balto.Cli.Abstraction;
using Balto.Cli.Client;
using Balto.Cli.Console;
using Balto.Cli.Handlers;
using Balto.Cli.Modules;
using Balto.Cli.Remote;
using Spectre.Console;
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
        private static IAnsiConsole Console;
        private static BaltoHttpClient BaltoHttpClient;

        private const int _successCode = 0;
        private const int _failureCode = 1;

        private const string _configModuleCommand = "config";

        private static async Task<int> Main(string[] args)
        {
            string debugArgs = "goal insert --title \" My new cool goal \" --description \"The description for my letest goal! \"";
            args = debugArgs.Split(" ");

            try
            {
                var arguments = new Arguments(args);

                await Initialization (arguments.GetModuleSelector == _configModuleCommand);

                await RunModule(arguments);

                await FileHandler.SaveClientConfigurationAsync(Client);

                return _successCode;
            }
            catch (Exception ex)
            {
                if (Console is null)
                {
                    System.Console.WriteLine(ex.Message);
                }
                else
                {
                    Console.WriteException(ex);
                }

                return _failureCode;
            }
        }

        private static async Task RunModule(IArguments args)
        {
            var moduleSelector = (!args.Any) ? "help" : args.GetModuleSelector;

            try
            {
                await Modules.Single(m => m.ModuleName.ToLower() == moduleSelector).Invoke(args);
            }
            catch (InvalidOperationException)
            {
                await Modules.Single(m => m.ModuleName.ToLower() == "help").Invoke(args);
            }
        }

        private static async Task Initialization(bool safeInitialization)
        {
            if (!FileHandler.CheckClientConfigurationFile())
            {
                var client = ClientConfiguration.Factory.Initialize();
                if (!await FileHandler.SaveClientConfigurationAsync(client))
                    throw new InvalidOperationException("Unable to save client configuration");
            }

            Client = FileHandler.GetClientConfiguration();

            if (!safeInitialization) BaltoHttpClient = await BaltoHttpClient.CreateInstance(Client);

            Console = AnsiConsole.Create(new AnsiConsoleSettings());

            Modules = new List<IModule>
            {
                new ConfigurationModule(Client, Console),
                new HelpModule(Console)
            };

            if (!safeInitialization)
            {
                Modules.AddRange(new List<IModule>
                {
                    new GoalModule(Client, BaltoHttpClient, Console)
                });
            }
        }
    }
}
