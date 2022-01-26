using Balto.Cli.Abstraction;
using Balto.Cli.Client;
using Balto.Cli.Remote;
using Spectre.Console;
using System;
using System.Threading.Tasks;

namespace Balto.Cli.Modules
{
    public class GoalModule : ModuleBase, IModule
    {
        private const string _moduleName = "goal";
        public string ModuleName => _moduleName;

        private readonly ClientConfiguration _clientConfiguration;
        private readonly BaltoHttpClient _balto;

        public GoalModule(ClientConfiguration clientConfiguration, BaltoHttpClient balto ,IAnsiConsole ansiConsole) : base(ansiConsole)
        {
            _clientConfiguration = clientConfiguration ??
                throw new ArgumentNullException(nameof(clientConfiguration));

            _balto = balto ??
                throw new ArgumentNullException(nameof(balto));
        }

        public async Task Invoke(IArguments args)
        {
            throw new NotImplementedException();
        }

        private async Task AddSubmodule(string[] args)
        {
            throw new NotImplementedException();
        }

        private async Task GetSubmodule(IArguments args)
        {
            throw new NotImplementedException();
        }

        private async Task DeleteSubmodule(string[] args)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateSubmodule(IArguments args)
        {
            throw new NotImplementedException();
        }

        protected override void ModuleUsage()
        {
            throw new NotImplementedException();
        }
    }
}
