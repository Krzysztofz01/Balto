using AutoMapper;
using Balto.Service;
using Balto.Service.Dto;
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

                var projectsMapped = mapper.Map<IEnumerable<ProjectGetView>>(projects);

                return Ok(projectsMapped);
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting user projects!");
                return StatusCode(500);
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
                projectMapped.OwnerId = user.Id;

                if (await projectService.Add(projectMapped))
                {
                    return Ok();
                }
                return Problem();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on posting user project!");
                return StatusCode(500);
            }
        }

        [HttpGet("{projectId}")]
        [Authorize]
        public async Task<ActionResult<ProjectGetView>> GetProjectByIdV1(long projectId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var project = await projectService.Get(projectId, user.Id);
                if (project is null) return NotFound();

                var projectMapped = mapper.Map<ProjectGetView>(project);
                return Ok(projectMapped);
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting user project by id!");
                return StatusCode(500);
            }
        }

        [HttpDelete("{projectId}")]
        [Authorize]
        public async Task<IActionResult> DeleteProjectByIdV1(long projectId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                if (await projectService.Delete(projectId, user.Id)) return Ok();
                return NotFound();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on deleting user project by id!");
                return StatusCode(500);
            }
        }

        [HttpPatch("{projectId}")]
        [Authorize]
        public async Task<IActionResult> UpdateProjectByIdV1(long projectId, [FromBody]ProjectPostView project)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var projectMapped = mapper.Map<ProjectDto>(project);
                projectMapped.OwnerId = user.Id;
                projectMapped.Id = projectId;

                await projectService.Update(projectMapped, user.Id);
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating user project by id!");
                return StatusCode(500);
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

                var tabelsMapped = mapper.Map<IEnumerable<ProjectTableGetView>>(tabels);

                return Ok(tabelsMapped);
            }
            catch (Exception e)
            { 
                logger.LogError(e, "System failure on getting user project tabels!");
                return StatusCode(500);
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

                if(await projectTableService.Add(tableMapped, projectId, user.Id))
                {
                    return Ok();
                }
                return Problem();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on posting user project table!");
                return StatusCode(500);
            }
        }

        [HttpGet("{projectId}/table/{tableId}")]
        [Authorize]
        public async Task<ActionResult<ProjectTableGetView>> GetTableByIdV1(long projectId, long tableId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var table = await projectTableService.Get(projectId, tableId, user.Id);
                if (table is null) return NotFound();

                var tableMapped = mapper.Map<ProjectTableGetView>(table);
                return Ok(tableMapped);
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting user table by id!");
                return StatusCode(500);
            }
        }

        [HttpDelete("{projectId}/table/{tableId}")]
        [Authorize]
        public async Task<IActionResult> DeleteTableByIdV1(long projectId, long tableId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                if (await projectTableService.Delete(projectId, tableId, user.Id)) return Ok();
                return NotFound();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on deleting user table by id!");
                return StatusCode(500);
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
                tableMapped.Id = tableId;

                await projectTableService.Update(tableMapped, projectId, user.Id);
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating user table by id!");
                return StatusCode(500);
            }
        }

        //Project table entry related

        [HttpPatch("{projectId}/table/{tableId}/order")]
        public async Task<IActionResult> UpdateEntryOrderV1(long projectId, long tableId, [FromBody] EntryOrder entryOrder)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                if (await projectTableEntryService.ChangeOrder(entryOrder.Order, projectId, tableId, user.Id)) return Ok();
                return Problem();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating user entry order!");
                return StatusCode(500);
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

                var entriesMapped = mapper.Map<IEnumerable<ProjectTableEntryGetView>>(entries);

                return Ok(entriesMapped);
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting user entries!");
                return StatusCode(500);
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

                if (await projectTableEntryService.Add(entryMapped, projectId, tableId, user.Id))
                {
                    return Ok();
                }
                return Problem();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on posting user entry!");
                return StatusCode(500);
            }
        }

        [HttpGet("{projectId}/table/{tableId}/entry/{entryId}")]
        [Authorize]
        public async Task<ActionResult<ProjectTableEntryGetView>> GetEntryByIdV1(long projectId, long tableId, long entryId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var entry = await projectTableEntryService.Get(projectId, tableId, entryId, user.Id);
                if (entry is null) return NotFound();

                var entryMapped = mapper.Map<ProjectTableEntryGetView>(entry);
                return Ok(entryMapped);
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on getting user entry by id!");
                return StatusCode(500);
            }
        }

        [HttpDelete("{projectId}/table/{tableId}/entry/{entryId}")]
        [Authorize]
        public async Task<IActionResult> DeleteEntryByIdV1(long projectId, long tableId, long entryId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                if (await projectTableEntryService.Delete(projectId, tableId, entryId, user.Id)) return Ok();
                return NotFound();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on deleting user entry by id!");
                return StatusCode(500);
            }
        }

        [HttpPatch("{projectId}/table/{tableId}/entry/{entryId}")]
        [Authorize]
        public async Task<IActionResult> UpdateEntryByIdV1(long projectId, long tableId, long entryId, [FromBody]ProjectTableEntryPostView entry)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                var entryMapped = mapper.Map<ProjectTableEntryDto>(entry);
                entryMapped.Id = entryId;

                await projectTableEntryService.Update(entryMapped, projectId, tableId, user.Id);
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating user entry by id!");
                return StatusCode(500);
            }
        }

        [HttpPatch("{projectId}/table/{tableId}/entry/{entryId}/state")]
        [Authorize]
        public async Task<IActionResult> UpdateEntryStateByIdV1(long projectId, long tableId, long entryId)
        {
            try
            {
                var user = await userService.GetUserFromPayload(User.Claims);

                await projectTableEntryService.ChangeState(projectId, tableId, entryId, user.Id);
                return Ok();
            }
            catch (Exception e)
            {
                logger.LogError(e, "System failure on updating user entry state by id!");
                return StatusCode(500);
            }
        }
    }
}
