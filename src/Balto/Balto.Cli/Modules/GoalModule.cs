using Balto.Application.Goals;
using Balto.Cli.Abstraction;
using Balto.Cli.Client;
using Balto.Cli.Remote;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using static Balto.Application.Goals.Dto;

namespace Balto.Cli.Modules
{
    public class GoalModule : ModuleBase, IModule
    {
        private const string _moduleName = "goal";
        public string ModuleName => _moduleName;

        private readonly ClientConfiguration _clientConfiguration;
        private readonly BaltoHttpClient _balto;

        private const string _uriBase = "api/v1/goal/";

        public GoalModule(ClientConfiguration clientConfiguration, BaltoHttpClient balto, IAnsiConsole ansiConsole) : base(ansiConsole)
        {
            _clientConfiguration = clientConfiguration ??
                throw new ArgumentNullException(nameof(clientConfiguration));

            _balto = balto ??
                throw new ArgumentNullException(nameof(balto));
        }

        public async Task Invoke(IArguments args)
        {
            var submodule = args.GetSubmoduleSelector;

            switch (submodule)
            {
                case "insert": await AddSubmodule(args); break;
                case "get": await GetSubmodule(args); break;
                case "delete": await DeleteSubmodule(args); break;
                case "update": await UpdateSubmodule(args); break;
                case "check": await StatusChangeSubmodule(args); break;

                case null: ModuleUsage(); break;
                default: ModuleUsage(); break;
            }
        }

        private async Task AddSubmodule(IArguments args)
        {
            string title = args.GetPropertyValueOrDefault("--title");

            var requestBody = JsonContent.Create(new Commands.V1.Create
            {
                Title = title
            });

            var (_, statusCode) = await _balto.AuthenticatedPostAsync<object>(_uriBase, requestBody);
            if (StatusOk(statusCode))
            {
                _console.Write(new Markup($"[green]Goal: [/] [italic lime]{title}[/] [green]created.[/]"));
                return;
            }

            _console.Write(new Markup($"[bold red]Failure on creating a new goal ({statusCode}).[/]"));
        }

        private async Task GetSubmodule(IArguments args)
        {
            var id = args.GetPropertyValueOrDefault("--id");
            if (id is not null)
            {
                var (goal, statusCodeGoal) = await _balto.AuthenticatedGetAsync<GoalDetails>($"{_uriBase}{id}");
                if (StatusOk(statusCodeGoal))
                {
                    PrintGoalSingle(goal);
                    return;
                }

                _console.Write(new Markup($"[bold red]Failure on retriving a goal ({statusCodeGoal}).[/]"));
                return;
            }

            var recurring = args.IsFlagSet("--recurring");
            var finished = args.IsFlagSet("--finished");
            var unfinished = args.IsFlagSet("--unfinished");

            if (true == finished == unfinished)
                throw new InvalidOperationException("Invalid flag combination.");

            string requestUri = recurring
                ? $"{_uriBase}recurring"
                : $"{_uriBase}nonrecurring";

            var (goals, statusCodeGoals) = await _balto.AuthenticatedGetAsync<IEnumerable<GoalSimple>>(requestUri);
            if (StatusOk(statusCodeGoals))
            {
                if (finished) goals = goals.Where(g => g.Finished);
                if (unfinished) goals = goals.Where(g => !g.Finished);

                PrintGoalCollection(goals);
                return;
            }

            _console.Write(new Markup($"[bold red]Failure on retriving goals ({statusCodeGoals}).[/]"));
        }

        private async Task DeleteSubmodule(IArguments args)
        {
            throw new NotImplementedException();
        }

        private async Task UpdateSubmodule(IArguments args)
        {
            throw new NotImplementedException();
        }

        private async Task StatusChangeSubmodule(IArguments args)
        {
            throw new NotImplementedException();
        }

        protected override void ModuleUsage()
        {
            throw new NotImplementedException();
        }

        private void PrintGoalCollection(IEnumerable<GoalSimple> goals)
        {
            throw new NotImplementedException();
        }

        private void PrintGoalSingle(GoalDetails goal)
        {
            throw new NotImplementedException();
        }
    }
}
