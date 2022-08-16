using Balto.API.Controllers.Base;
using Balto.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using static Balto.Application.Projects.Commands;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/project")]
    [ApiVersion("1.0")]
    public class ProjectCommandController : CommandController
    {
        private readonly IProjectService _projectService;

        public ProjectCommandController(IProjectService projectService, ILogger<ProjectCommandController> logger) : base(logger)
        {
            _projectService = projectService ??
                throw new ArgumentNullException(nameof(projectService));
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1.Create command) =>
            await Command(command, _projectService.Handle);

        [HttpPut]
        public async Task<IActionResult> Update(V1.Update command) =>
            await Command(command, _projectService.Handle);

        [HttpDelete]
        public async Task<IActionResult> Delete(V1.Delete command) =>
            await Command(command, _projectService.Handle);

        [HttpPost("ticket")]
        public async Task<IActionResult> PushTicket(V1.PushTicket command) =>
            await Command(command, _projectService.Handle);

        [HttpPost("contributor")]
        public async Task<IActionResult> AddContributor(V1.AddContributor command) =>
            await Command(command, _projectService.Handle);

        [HttpDelete("contributor")]
        public async Task<IActionResult> DeleteContributor(V1.DeleteContributor command) =>
            await Command(command, _projectService.Handle);

        [HttpPut("contributor")]
        public async Task<IActionResult> UpdateContributor(V1.UpdateContributor command) =>
            await Command(command, _projectService.Handle);

        [HttpPut("contributor/leave")]
        public async Task<IActionResult> LeaveAsContributor(V1.LeaveAsContributor command) =>
            await Command(command, _projectService.Handle);

        [HttpPost("table")]
        public async Task<IActionResult> AddTable(V1.AddTable command) =>
            await Command(command, _projectService.Handle);

        [HttpDelete("table")]
        public async Task<IActionResult> DeleteTable(V1.DeleteTable command) =>
            await Command(command, _projectService.Handle);

        [HttpPut("table")]
        public async Task<IActionResult> UpdateTable(V1.UpdateTable command) =>
            await Command(command, _projectService.Handle);

        [HttpPut("table/order")]
        public async Task<IActionResult> ChangeTableOrdinalNumbers(V1.TableOrdinalNumbersChanged command) =>
            await Command(command, _projectService.Handle);

        [HttpPost("task")]
        public async Task<IActionResult> AddTask(V1.AddTask command) =>
            await Command(command, _projectService.Handle);

        [HttpPut("task")]
        public async Task<IActionResult> UpdateTask(V1.UpdateTask command) =>
            await Command(command, _projectService.Handle);

        [HttpDelete("task")]
        public async Task<IActionResult> DeleteTask(V1.DeleteTask command) =>
            await Command(command, _projectService.Handle);

        [HttpPut("task/status")]
        public async Task<IActionResult> ChangeTaskStatus(V1.ChangeTaskStatus command) =>
            await Command(command, _projectService.Handle);

        [HttpPut("task/tag/assign")]
        public async Task<IActionResult> AssignTag(V1.TaskTagAssign command) =>
            await Command(command, _projectService.Handle);

        [HttpPut("task/tag/unassign")]
        public async Task<IActionResult> UnassignTag(V1.TaskTagUnassign command) =>
            await Command(command, _projectService.Handle);
    }
}
