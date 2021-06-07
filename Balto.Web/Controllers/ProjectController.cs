using AutoMapper;
using Balto.Service;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using Balto.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/project")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly IProjectTableService projectTableService;
        private readonly IProjectTableEntryService projectTableEntryService;
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(
            IProjectService projectService,
            IProjectTableService projectTableService,
            IProjectTableEntryService projectTableEntryService,
            IUserService userService,
            IMapper mapper,
            ILogger<ProjectController> logger)
        {
            this.projectService = projectService ??
                throw new ArgumentNullException(nameof(projectService));

            this.projectTableService = projectTableService ??
                throw new ArgumentNullException(nameof(projectTableService));

            this.projectTableEntryService = projectTableEntryService ??
                throw new ArgumentNullException(nameof(projectTableEntryService));

            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            this.logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }

        //Project related

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProjectGetView>>> GetAllProjectsV1()
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var projects = await projectService.GetAll(user.Id);

                var projectsMapped = mapper.Map<IEnumerable<ProjectGetView>>(projects.Result());

                return Ok(projectsMapped);
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting user projects!");
                return Problem();
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> PostProjectV1(ProjectPostView project)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var projectMapped = mapper.Map<ProjectDto>(project);

                var result = await projectService.Add(projectMapped, user.Id);

                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on posting user project!");
                return Problem();
            }
        }

        [HttpGet("{projectId}")]
        [Authorize]
        public async Task<ActionResult<ProjectGetView>> GetProjectByIdV1(long projectId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await projectService.Get(projectId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess)
                {
                    var projectMapped = mapper.Map<ProjectGetView>(result.Result());
                    return Ok(projectMapped);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting user project by id!");
                return Problem();
            }
        }

        [HttpDelete("{projectId}")]
        [Authorize]
        public async Task<IActionResult> DeleteProjectByIdV1(long projectId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await projectService.Delete(projectId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on deleting user project by id!");
                return Problem();
            }
        }

        [HttpPatch("{projectId}")]
        [Authorize]
        public async Task<IActionResult> UpdateProjectByIdV1(long projectId, [FromBody]ProjectPatchView project)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var projectMapped = mapper.Map<ProjectDto>(project);

                var result = await projectService.Update(projectMapped, projectId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating user project by id!");
                return Problem();
            }
        }

        [HttpPost("{projectId}/invite")]
        [Authorize]
        public async Task<IActionResult> PostInviteV1(long projectId, [FromBody]CollaborationInvitation invitation)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await projectService.InviteUser(projectId, invitation.Email, user.Id);

                if (result.Status() == ResultStatus.Sucess) return Ok();
                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.NotPermited) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on posting project collaboration invitation!");
                return Problem();
            }
        }

        [HttpPatch("{projectId}/leave")]
        [Authorize]
        public async Task<IActionResult> PostLeaveV1(long projectId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await projectService.Leave(projectId, user.Id);
                
                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.NotPermited) return Forbid();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on leaving a project!");
                return Problem();
            }
        }

        //Project table related

        [HttpGet("{projectId}/table")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProjectTableGetView>>> GetAllTabelsV1(long projectId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var tabels = await projectTableService.GetAll(projectId, user.Id);

                var tabelsMapped = mapper.Map<IEnumerable<ProjectTableGetView>>(tabels.Result());

                return Ok(tabelsMapped);
            }
            catch (Exception e)
            { 
                logger.LogError(e, "System failure on getting user project tabels!");
                return Problem();
            }
        }

        [HttpPost("{projectId}/table")]
        [Authorize]
        public async Task<IActionResult> PostTableV1(long projectId, [FromBody]ProjectTablePostView table)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var tableMapped = mapper.Map<ProjectTableDto>(table);

                var result = await projectTableService.Add(tableMapped, projectId, user.Id);

                if (result.Status() == ResultStatus.Sucess) return Ok();
                if (result.Status() == ResultStatus.NotPermited) return Forbid();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on posting user project table!");
                return Problem();
            }
        }

        [HttpGet("{projectId}/table/{tableId}")]
        [Authorize]
        public async Task<ActionResult<ProjectTableGetView>> GetTableByIdV1(long projectId, long tableId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await projectTableService.Get(projectId, tableId, user.Id);
                
                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if(result.Status() == ResultStatus.Sucess)
                {
                    var tableMapped = mapper.Map<ProjectTableGetView>(result.Result());
                    return Ok(tableMapped);
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting user table by id!");
                return Problem();
            }
        }

        [HttpDelete("{projectId}/table/{tableId}")]
        [Authorize]
        public async Task<IActionResult> DeleteTableByIdV1(long projectId, long tableId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await projectTableService.Delete(projectId, tableId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on deleting user table by id!");
                return Problem();
            }
        }

        [HttpPatch("{projectId}/table/{tableId}")]
        [Authorize]
        public async Task<IActionResult> UpdateTableByIdB1(long projectId, long tableId, [FromBody]ProjectTablePostView table)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var tableMapped = mapper.Map<ProjectTableDto>(table);

                var result = await projectTableService.Update(tableMapped, projectId, tableId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating user table by id!");
                return Problem();
            }
        }

        //Project table entry related

        [HttpPatch("{projectId}/table/{tableId}/order")]
        public async Task<IActionResult> UpdateEntryOrderV1(long projectId, long tableId, [FromBody] EntryOrder entryOrder)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await projectTableEntryService.ChangeOrder(entryOrder.Order, projectId, tableId, user.Id);

                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating user entry order!");
                return Problem();
            }
        }

        [HttpGet("{projectId}/table/{tableId}/entry")]
        [Authorize]
        public async Task<ActionResult<IEnumerable<ProjectTableEntryGetView>>> GetAllEntriesV1(long projectId, long tableId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var entries = await projectTableEntryService.GetAll(projectId, tableId, user.Id);

                var entriesMapped = mapper.Map<IEnumerable<ProjectTableEntryGetView>>(entries.Result());

                return Ok(entriesMapped);
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting user entries!");
                return Problem();
            }
        }

        [HttpPost("{projectId}/table/{tableId}/entry")]
        [Authorize]
        public async Task<IActionResult> PostEntryV1(long projectId, long tableId, [FromBody]ProjectTableEntryPostView entry)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var entryMapped = mapper.Map<ProjectTableEntryDto>(entry);

                var result = await projectTableEntryService.Add(entryMapped, projectId, tableId, user.Id);

                if (result.Status() == ResultStatus.Sucess) return Ok();
                if (result.Status() == ResultStatus.NotPermited) return Forbid();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on posting user entry!");
                return Problem();
            }
        }

        [HttpGet("{projectId}/table/{tableId}/entry/{entryId}")]
        [Authorize]
        public async Task<ActionResult<ProjectTableEntryGetView>> GetEntryByIdV1(long projectId, long tableId, long entryId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await projectTableEntryService.Get(projectId, tableId, entryId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess)
                {
                    var entryMapped = mapper.Map<ProjectTableEntryGetView>(result.Result());
                    return Ok(entryMapped);
                }

                return BadRequest();      
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting user entry by id!");
                return Problem();
            }
        }

        [HttpDelete("{projectId}/table/{tableId}/entry/{entryId}")]
        [Authorize]
        public async Task<IActionResult> DeleteEntryByIdV1(long projectId, long tableId, long entryId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await projectTableEntryService.Delete(projectId, tableId, entryId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on deleting user entry by id!");
                return Problem();
            }
        }

        [HttpPatch("{projectId}/table/{tableId}/entry/{entryId}")]
        [Authorize]
        public async Task<IActionResult> UpdateEntryByIdV1(long projectId, long tableId, long entryId, [FromBody]ProjectTableEntryPatchView entry)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var entryMapped = mapper.Map<ProjectTableEntryDto>(entry);

                var result = await projectTableEntryService.Update(entryMapped, projectId, tableId, entryId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating user entry by id!");
                return Problem();
            }
        }

        [HttpPatch("{projectId}/table/{tableId}/entry/{entryId}/state")]
        [Authorize]
        public async Task<IActionResult> UpdateEntryStateByIdV1(long projectId, long tableId, long entryId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await projectTableEntryService.ChangeState(projectId, tableId, entryId, user.Id);

                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating user entry state by id!");
                return Problem();
            }
        }

        [HttpPatch("{projectId}/table/{tableId}/entry/{entryId}/move/{newTableId}")]
        public async Task<IActionResult> MoveEntryToTableByIdV1(long projectId, long tableId, long entryId, long newTableId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var result = await projectTableEntryService.MoveToTable(projectId, tableId, entryId, newTableId, user.Id);

                if (result.Status() == ResultStatus.NotFound) return NotFound();
                if (result.Status() == ResultStatus.Sucess) return Ok();
                return BadRequest();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on moving entry between tables by id!");
                return Problem();
            }
        }
    }
}
