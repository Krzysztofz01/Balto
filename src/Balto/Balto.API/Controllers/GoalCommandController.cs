using Balto.API.Utility;
using Balto.Application.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Balto.Application.Goals.Commands;

namespace Balto.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/goal")]
    [ApiVersion("1.0")]
    [Authorize]
    public class GoalCommandController : ControllerBase
    {
        private readonly IGoalService _goalService;

        public GoalCommandController(IGoalService goalService)
        {
            _goalService = goalService ??
                throw new ArgumentNullException(nameof(goalService));
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1.Create command) =>
            await RequestHandler.Command(command, _goalService.Handle);

        [HttpDelete]
        public async Task<IActionResult> Delete(V1.Delete command) =>
            await RequestHandler.Command(command, _goalService.Handle);

        [HttpPut]
        public async Task<IActionResult> Update(V1.Update command) =>
            await RequestHandler.Command(command, _goalService.Handle);

        [HttpPut("state")]
        public async Task<IActionResult> StateChange(V1.StateChange command) =>
            await RequestHandler.Command(command, _goalService.Handle);
    }
}
