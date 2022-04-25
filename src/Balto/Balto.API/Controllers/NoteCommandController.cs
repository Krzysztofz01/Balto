using Balto.API.Controllers.Base;
using Balto.Application.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using static Balto.Application.Notes.Commands;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/note")]
    [ApiVersion("1.0")]
    public class NoteCommandController : CommandController
    {
        private readonly INoteService _noteService;

        public NoteCommandController(INoteService noteService) : base()
        {
            _noteService = noteService ??
                throw new ArgumentNullException(nameof(noteService));
        }

        [HttpPost]
        public async Task<IActionResult> Create(V1.Create command) =>
            await Command(command, _noteService.Handle);

        [HttpPut]
        public async Task<IActionResult> Update(V1.Update command) =>
            await Command(command, _noteService.Handle);

        [HttpDelete]
        public async Task<IActionResult> Delete(V1.Delete command) =>
            await Command(command, _noteService.Handle);

        [HttpPost("contributor")]
        public async Task<IActionResult> AddContributor(V1.AddContributor command) =>
            await Command(command, _noteService.Handle);

        [HttpDelete("contributor")]
        public async Task<IActionResult> DeleteContributor(V1.DeleteContributor command) =>
            await Command(command, _noteService.Handle);

        [HttpPut("contributor")]
        public async Task<IActionResult> UpdateContributor(V1.UpdateContributor command) =>
            await Command(command, _noteService.Handle);

        [HttpPut("contributor/leave")]
        public async Task<IActionResult> LeaveAsContributor(V1.LeaveAsContributor command) =>
            await Command(command, _noteService.Handle);

        [HttpPut("tag/assign")]
        public async Task<IActionResult> TagAssign(V1.TagAssign command) =>
            await Command(command, _noteService.Handle);

        [HttpPut("tag/unassign")]
        public async Task<IActionResult> TagUnassign(V1.TagUnassign command) =>
            await Command(command, _noteService.Handle);

        [HttpPost("snapshot")]
        public async Task<IActionResult> SnapshotCreate(V1.SnapshotCreate command) =>
            await Command(command, _noteService.Handle);

        [HttpDelete("snapshot")]
        public async Task<IActionResult> SnapshotDelete(V1.SnapshotDelete command) =>
            await Command(command, _noteService.Handle);
    }
}
