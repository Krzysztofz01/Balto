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
    [Route("api/v{version:apiVersion}/objective")]
    [ApiVersion("1.0")]
    public class ObjectiveController : ControllerBase
    {
        private readonly IObjectiveService objectiveService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ILogger<ObjectiveController> logger;

        public ObjectiveController(
            IObjectiveService objectiveService,
            IUserService userService,
            IMapper mapper,
            ILogger<ObjectiveController> logger)
        {
            this.objectiveService = objectiveService ??
                throw new ArgumentNullException(nameof(objectiveService));

            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            this.logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ObjectiveGetView>>> GetAllV1()
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var userObjectives = await objectiveService.GetAll(user.Id);

                var userObjectivesMapped = mapper.Map<IEnumerable<ObjectiveGetView>>(userObjectives);

                return Ok(userObjectivesMapped);
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on getting user objectives!");
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostOneV1(ObjectivePostView objective)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var objectiveMapped = mapper.Map<ObjectiveDto>(objective);
                objectiveMapped.Id = user.Id;

                if(await objectiveService.Add(objectiveMapped))
                {
                    return Ok();
                }
                return Problem();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on posting user objective!");
                return StatusCode(500);
            }
        }

        [HttpGet("{objectiveId}")]
        [Authorize]
        public async Task<ActionResult<ObjectiveGetView>> GetByIdV1(long objectiveId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var objective = await objectiveService.Get(objectiveId, user.Id);
                if (objective is null) return NotFound();

                var objectiveMapped = mapper.Map<ObjectiveGetView>(objective);
                return Ok(objectiveMapped);
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on getting user objective by id!");
                return StatusCode(500);
            }
        }

        [HttpDelete("{objectiveId}")]
        [Authorize]
        public async Task<IActionResult> DeleteByIdV1(long objectiveId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                if (await objectiveService.Delete(objectiveId, user.Id)) return Ok();
                return NotFound();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on deleting user objective by id!");
                return StatusCode(500);
            }
        }

        [HttpPatch("{objectiveId}/state")]
        [Authorize]
        public async Task<IActionResult> ChangeStateByIdV1(long objectiveId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                if (await objectiveService.ChangeState(objectiveId, user.Id)) return Ok();
                return NotFound();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on changing state of users objective by id!");
                return StatusCode(500);
            }
        }
    }
}
