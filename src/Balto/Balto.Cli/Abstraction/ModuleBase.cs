using Spectre.Console;
using System;
using System.Net;

namespace Balto.Cli.Abstraction
{
    public abstract class ModuleBase
    {
        protected readonly IAnsiConsole _console;

        public ModuleBase(IAnsiConsole ansiConsole) =>
            _console = ansiConsole ??
                throw new ArgumentNullException(nameof(ansiConsole));

        protected abstract void ModuleUsage();

        protected static bool StatusOk(HttpStatusCode statusCode) => statusCode == HttpStatusCode.OK;
    }
}
