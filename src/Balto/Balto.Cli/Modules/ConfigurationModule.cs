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

        public async Task Invoke(IArguments args)
        {
            if (args.PropertyCount < 2 || args.IsHelp)
            {
                ModuleUsage();
                return;
            }

            try
            {
                var property = args.GetPropertyValue("--option");
                var value = args.GetPropertyValue("--value");

                _clientConfiguration.ApplyConfiguration(property, value);

                _console.Write(new Markup($"[green]Value of[/] [italic lime]{property}[/] [green]changed successfull.[/]"));
            } 
            catch (InvalidOperationException)
            {
                _console.Write(new Markup($"[bold red]Given option is not available.[/]"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        protected override void ModuleUsage()
        {
            _console.Write(new Markup($"[teal][bold]Usage:[/] balto config --option <property_name> --value <property_value>[/]"));
        }
    }
}
