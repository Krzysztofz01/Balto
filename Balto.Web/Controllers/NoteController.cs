using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Balto.Service;
using Balto.Service.Dto;
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

                var userNotesMapped = mapper.Map<IEnumerable<NoteGetView>>(userNotes);

                return Ok(userNotesMapped);
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on getting user notes!");
                return StatusCode(500);
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
                noteMapped.OwnerId = user.Id;

                if(await noteService.Add(noteMapped))
                {
                    return Ok();
                }
                return Problem();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on posting user note!");
                return StatusCode(500);
            }
        }

        [HttpGet("{noteId}")]
        [Authorize]
        public async Task<ActionResult<NoteGetView>> GetByIdV1(long noteId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var note = await noteService.Get(noteId, user.Id);
                if (note is null) return NotFound();

                var noteMapped = mapper.Map<NoteGetView>(note);
                return Ok(noteMapped);
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on getting user note by id!");
                return StatusCode(500);
            }
        }

        [HttpDelete("{noteId}")]
        [Authorize]
        public async Task<IActionResult> DeleteByIdV1(long noteId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                if (await noteService.Delete(noteId, user.Id)) return Ok();
                return NotFound();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on deleting user note by id!");
                return StatusCode(500);
            }
        }

        [HttpPatch("{noteId}")]
        [Authorize]
        public async Task<IActionResult> UpdateByIdV1(long noteId, [FromBody]NotePostView note)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var noteMapped = mapper.Map<NoteDto>(note);
                noteMapped.Id = noteId;

                await noteService.Update(noteMapped, user.Id);
                return Ok();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on updating user note by id!");
                return StatusCode(500);
            }
        }
    }
}
