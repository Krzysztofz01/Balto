using Balto.Cli.Abstraction;
using Balto.Cli.Client;
using Spectre.Console;
using System;
using System.Threading.Tasks;

namespace Balto.Cli.Modules
{
    public class ConfigurationModule : ModuleBase, IModule
    {
        private const string _moduleName = "config";
        public string ModuleName => _moduleName;

        private readonly ClientConfiguration _clientConfiguration;

        public ConfigurationModule(ClientConfiguration clientConfiguration, IAnsiConsole ansiConsole) : base(ansiConsole)
        {
            _clientConfiguration = clientConfiguration ??
                throw new ArgumentNullException(nameof(clientConfiguration));
        }

        public async Task Invoke(string[] args)
        {
            if (args.Length < 2) ModuleUsage();

            string property = args[0];
            string value = args[1];

            try
            {
                _clientConfiguration.ApplyConfiguration(property, value);

                _console.Write(new Markup($"[green]Value of[/] [italic lime]{property}[/] [green]changed successfull.[/]"));
            } 
            catch (InvalidOperationException)
            {
                _console.Write(new Markup($"[bold red]Option[/] [italic darkred]{property}[/] [bold red]is not available.[/]"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void ModuleUsage()
        {
            throw new NotImplementedException();
        }
    }
}
