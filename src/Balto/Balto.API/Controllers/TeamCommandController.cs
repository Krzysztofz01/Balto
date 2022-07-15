using Balto.API.Controllers.Base;
using Balto.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Balto.Application.Teams.Commands;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/team")]
    [ApiVersion("1.0")]
    public class TeamCommandController : CommandController
    {
        private readonly ITeamService _teamService;

        public TeamCommandController(ITeamService teamService) : base()
        {
            _teamService = teamService ??
                throw new ArgumentNullException(nameof(teamService));
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1.Create command) =>
            await Command(command, _teamService.Handle);

        [HttpDelete]
        public async Task<IActionResult> Delete(V1.Delete command) =>
            await Command(command, _teamService.Handle);

        [HttpPost("member")]
        public async Task<IActionResult> AddMember(V1.AddMember command) =>
            await Command(command, _teamService.Handle);

        [HttpDelete("member")]
        public async Task<IActionResult> DeleteMember(V1.DeleteMember command) =>
            await Command(command, _teamService.Handle);
    }
}
