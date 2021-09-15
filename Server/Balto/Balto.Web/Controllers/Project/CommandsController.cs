using Balto.Application.Aggregates.Project;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Balto.Web.Controllers.Project
{
    [ApiController]
    [Route("api/v{version:apiVersion}/project")]
    [ApiVersion("1.0")]
    [Authorize]
    public class CommandsController : ControllerBase
    {
        private readonly ProjectService _projectService;

        public CommandsController(ProjectService projectService)
        {
            _projectService = projectService ??
                throw new ArgumentNullException(nameof(projectService));
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(Commands.V1.ProjectAdd request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPut]
        public async Task<IActionResult> UpdateProject(Commands.V1.ProjectUpdate request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(Commands.V1.ProjectDelete request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPost("contributor")]
        public async Task<IActionResult> AddProjectContributor(Commands.V1.ProjectAddContributor request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpDelete("contributor")]
        public async Task<IActionResult> DeleteProjectContributor(Commands.V1.ProjectDeleteContributor request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPut("leave")]
        public async Task<IActionResult> LeaveProject(Commands.V1.ProjectLeave request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPut("ticket/status")]
        public async Task<IActionResult> ChangeProjectTicketStatus(Commands.V1.ProjectChangeTicketStatus request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPost("ticket")]
        public async Task<IActionResult> ChangeProjectTicketStatus(Commands.V1.ProjectAddTicket request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPost("table")]
        public async Task<IActionResult> AddProjectTable(Commands.V1.ProjectAddTable request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPut("table")]
        public async Task<IActionResult> UpdateProjectTable(Commands.V1.ProjectUpdateTable request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpDelete("table")]
        public async Task<IActionResult> DeleteProjectTable(Commands.V1.ProjectDeleteTable request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPost("card")]
        public async Task<IActionResult> AddProjectTableCard(Commands.V1.ProjectAddCard request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPut("card")]
        public async Task<IActionResult> UpdateProjectTableCard(Commands.V1.ProjectUpdateCard request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpDelete("card")]
        public async Task<IActionResult> DeleteProjectTableCard(Commands.V1.ProjectDeleteCard request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPut("card/status")]
        public async Task<IActionResult> ChangeProjectTableCardStatus(Commands.V1.ProjectChangeCardStatus request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPut("card/order")]
        public async Task<IActionResult> UpdateProjectTableCardOrder(Commands.V1.ProjectChangeCardsOrdinalNumber request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpPost("comment")]
        public async Task<IActionResult> AddProjectTableCardComment(Commands.V1.ProjectAddComment request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);

        [HttpDelete("comment")]
        public async Task<IActionResult> DeleteProjectTableCardComment(Commands.V1.ProjectDeleteComment request) =>
            await RequestHandler.HandleCommand(request, _projectService.Handle);
    }
}
