using Balto.API.Controllers.Base;
using Balto.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Balto.Application.Goals.Commands;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/goal")]
    [ApiVersion("1.0")]
    public class GoalCommandController : CommandController
    {
        private readonly IGoalService _goalService;

        public GoalCommandController(IGoalService goalService) : base()
        {
            _goalService = goalService ??
                throw new ArgumentNullException(nameof(goalService));
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1.Create command) =>
            await Command(command, _goalService.Handle);

        [HttpDelete]
        public async Task<IActionResult> Delete(V1.Delete command) =>
            await Command(command, _goalService.Handle);

        [HttpPut]
        public async Task<IActionResult> Update(V1.Update command) =>
            await Command(command, _goalService.Handle);

        [HttpPut("status")]
        public async Task<IActionResult> StatusChange(V1.StatusChange command) =>
            await Command(command, _goalService.Handle);
    }
}
