using Balto.Application.Aggregates.Note;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Balto.Web.Controllers.Note
{
    [ApiController]
    [Route("api/v{version:apiVersion}/note")]
    [ApiVersion("1.0")]
    [Authorize]
    public class CommandsController : ControllerBase
    {
        private readonly NoteService _noteService;

        public CommandsController(NoteService noteService)
        {
            _noteService = noteService ??
                throw new ArgumentNullException(nameof(noteService));
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(Commands.V1.NoteAdd request) =>
            await RequestHandler.HandleCommand(request, _noteService.Handle);

        [HttpDelete]
        public async Task<IActionResult> DeleteNote(Commands.V1.NoteDelete request) =>
            await RequestHandler.HandleCommand(request, _noteService.Handle);

        [HttpPut]
        public async Task<IActionResult> UpdateNote(Commands.V1.NoteUpdate request) =>
            await RequestHandler.HandleCommand(request, _noteService.Handle);

        [HttpPut("publish")]
        public async Task<IActionResult> ChangeNotePublication(Commands.V1.NoteChangePublication request) =>
            await RequestHandler.HandleCommand(request, _noteService.Handle);

        [HttpPost("contributor")]
        public async Task<IActionResult> AddNoteContributor(Commands.V1.NoteAddContributor request) =>
            await RequestHandler.HandleCommand(request, _noteService.Handle);

        [HttpDelete("contributor")]
        public async Task<IActionResult> DeleteNoteContributor(Commands.V1.NoteDeleteContributor request) =>
            await RequestHandler.HandleCommand(request, _noteService.Handle);

        [HttpPut("leave")]
        public async Task<IActionResult> LeaveNote(Commands.V1.NoteLeave request) =>
            await RequestHandler.HandleCommand(request, _noteService.Handle);
    }
}
