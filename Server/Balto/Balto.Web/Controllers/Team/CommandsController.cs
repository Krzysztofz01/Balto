using Balto.Application.Aggregates.Team;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Balto.Web.Controllers.Team
{
    [ApiController]
    [Route("api/v{version:apiVersion}/team")]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Leader")]
    public class CommandsController : ControllerBase
    {
        private readonly TeamService _teamService;

        public CommandsController(TeamService teamService)
        {
            _teamService = teamService ??
                throw new ArgumentNullException(nameof(teamService));
        }

        [HttpPost]
        public async Task<IActionResult> AddTeam(Commands.V1.TeamCreate request) =>
            await RequestHandler.HandleCommand(request, _teamService.Handle);

        [HttpPut]
        public async Task<IActionResult> UpdateTeam(Commands.V1.TeamUpdate request) =>
            await RequestHandler.HandleCommand(request, _teamService.Handle);

        [HttpDelete]
        public async Task<IActionResult> DeleteTeam(Commands.V1.TeamDelete request) =>
            await RequestHandler.HandleCommand(request, _teamService.Handle);
    }
}
