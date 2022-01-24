using Spectre.Console;
using System;

namespace Balto.Cli.Abstraction
{
    public abstract class ModuleBase
    {
        protected readonly IAnsiConsole _console;

        public ModuleBase(IAnsiConsole ansiConsole) =>
            _console = ansiConsole ??
                throw new ArgumentNullException(nameof(ansiConsole));

        protected abstract void ModuleUsage();
    }
}
