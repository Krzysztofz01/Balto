using Balto.Cli.Abstraction;
using Spectre.Console;
using System;
using System.Threading.Tasks;

namespace Balto.Cli.Modules
{
    public class HelpModule : ModuleBase, IModule
    {
        public HelpModule(IAnsiConsole ansiConsole) : base(ansiConsole)
        {
        }

        private const string _moduleName = "help";
        public string ModuleName => _moduleName;

        public Task Invoke(IArguments args)
        {
            throw new NotImplementedException();
        }

        protected override void ModuleUsage()
        {
            throw new NotImplementedException();
        }
    }
}
