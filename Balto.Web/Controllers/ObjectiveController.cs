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

                var userObjectivesMapped = mapper.Map<IEnumerable<ObjectiveGetView>>(userObjectives.Result());

                return Ok(userObjectivesMapped);
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on getting user objectives!");
                return Problem();
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

                var result = await objectiveService.Add(objectiveMapped, user.Id);

                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on posting user objective!");
                return Problem();
            }
        }

        [HttpGet("{objectiveId}")]
        [Authorize]
        public async Task<ActionResult<ObjectiveGetView>> GetByIdV1(long objectiveId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await objectiveService.Get(objectiveId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess)
                {
                    var objectiveMapped = mapper.Map<ObjectiveGetView>(result.Result());
                    return Ok(objectiveMapped);
                }

                return BadRequest();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on getting user objective by id!");
                return Problem();
            }
        }

        [HttpDelete("{objectiveId}")]
        [Authorize]
        public async Task<IActionResult> DeleteByIdV1(long objectiveId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await objectiveService.Delete(objectiveId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on deleting user objective by id!");
                return Problem();
            }
        }

        [HttpPatch("{objectiveId}/state")]
        [Authorize]
        public async Task<IActionResult> ChangeStateByIdV1(long objectiveId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await objectiveService.ChangeState(objectiveId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch(Exception e)
            {
                logger.LogError(e, "System failure on changing state of users objective by id!");
                return Problem();
            }
        }
    }
}
