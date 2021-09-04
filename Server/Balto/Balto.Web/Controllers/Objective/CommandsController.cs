using Balto.Application.Aggregates.Objectives;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Balto.Web.Controllers.Objective
{
    [ApiController]
    [Route("api/v{version:apiVersion}/objective")]
    [ApiVersion("1.0")]
    public class CommandsController : ControllerBase
    {
        private readonly ObjectiveService _objectiveService;

        public CommandsController(ObjectiveService objectiveService)
        {
            _objectiveService = objectiveService ??
                throw new ArgumentNullException(nameof(objectiveService));
        }

        [HttpPost]
        public async Task<IActionResult> AddObjective(Commands.V1.ObjectiveAdd request) =>
            await RequestHandler.HandleCommand(request, _objectiveService.Handle);

        [HttpDelete]
        public async Task<IActionResult> DeleteObjective(Commands.V1.ObjectiveDelete request) =>
            await RequestHandler.HandleCommand(request, _objectiveService.Handle);

        [HttpPut]
        public async Task<IActionResult> UpdateObjective(Commands.V1.ObjectiveUpdate request) =>
            await RequestHandler.HandleCommand(request, _objectiveService.Handle);

        [HttpPut("state")]
        public async Task<IActionResult> ChangeObjectiveState(Commands.V1.ObjectiveStateChange request) =>
            await RequestHandler.HandleCommand(request, _objectiveService.Handle);
    }
}
