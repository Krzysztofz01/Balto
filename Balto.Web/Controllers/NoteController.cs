using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Balto.Service;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using Balto.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Balto.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/note")]
    [ApiVersion("1.0")]
    public class NoteController : ControllerBase
    {
        private readonly INoteService noteService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ILogger<NoteController> logger;

        public NoteController(
            INoteService noteService,
            IUserService userService,
            IMapper mapper, 
            ILogger<NoteController> logger)
        {
            this.noteService = noteService ??
                throw new ArgumentNullException(nameof(noteService));

            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            this.logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<NoteGetView>>> GetAllV1()
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var userNotes = await noteService.GetAll(user.Id);

                var userNotesMapped = mapper.Map<IEnumerable<NoteGetView>>(userNotes.Result());

                return Ok(userNotesMapped);
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on getting user notes!");
                return Problem();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostOneV1(NotePostView note)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var noteMapped = mapper.Map<NoteDto>(note);

                var result = await noteService.Add(noteMapped, user.Id);

                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on posting user note!");
                return Problem();
            }
        }


        [HttpPost("{noteId}/invite")]
        [Authorize]
        public async Task<IActionResult> PostInviteV1(long noteId, [FromBody]CollaborationInvitation invitation)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await noteService.InviteUser(noteId, invitation.Email, user.Id);

                if (result.Status() == ResultStatus.Sucess) return Ok();
                if (result.Status() == ResultStatus.NotPermited) return Forbid();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on posting objective collaboration invitation!");
                return Problem();
            }
        }

        [HttpGet("{noteId}")]
        [Authorize]
        public async Task<ActionResult<NoteGetView>> GetByIdV1(long noteId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await noteService.Get(noteId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess)
                {
                    var noteMapped = mapper.Map<NoteGetView>(result.Result());
                    return Ok(noteMapped);
                }

                return BadRequest();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on getting user note by id!");
                return Problem();
            }
        }

        [HttpDelete("{noteId}")]
        [Authorize]
        public async Task<IActionResult> DeleteByIdV1(long noteId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await noteService.Delete(noteId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on deleting user note by id!");
                return Problem();
            }
        }

        [HttpPatch("{noteId}")]
        [Authorize]
        public async Task<IActionResult> UpdateByIdV1(long noteId, [FromBody]NotePatchView note)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var noteMapped = mapper.Map<NoteDto>(note);

                var result = await noteService.Update(noteMapped, noteId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on updating user note by id!");
                return StatusCode(500);
            }
        }
    }
}
