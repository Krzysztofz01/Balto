using AutoMapper;
using Balto.API.Controllers.Base;
using Balto.Application.Projects;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Balto.Application.Projects.Dto;

namespace Balto.API.Controllers
{
    [Route("api/v{version:apiVersion}/project")]
    [ApiVersion("1.0")]
    public class ProjectQueryController : QueryController
    {
        public ProjectQueryController(IDataAccess dataAccess, IMapper mapper, IMemoryCache memoryCache) : base(dataAccess, mapper, memoryCache)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects()
        {
            var response = await _dataAccess.Projects.GetAllProjects();

            var mappedResponse = MapToDto<IEnumerable<ProjectSimple>>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectById(Guid projectId)
        {
            var response = await _dataAccess.Projects.GetProjectById(projectId);

            var mappedResponse = MapToDto<ProjectDetails>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("{projectId}/tables")]
        public async Task<IActionResult> GetAllTables(Guid projectId)
        {
            var response = await _dataAccess.Projects.GetAllTables(projectId);

            var mappedResponse = MapToDto<IEnumerable<TableSimple>>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("tables/{tableId}")]
        public async Task<IActionResult> GetTableById(Guid tableId)
        {
            var response = await _dataAccess.Projects.GetTableById(tableId);

            var mappedResponse = MapToDto<TableDetails>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("tables/{tableId}/tasks")]
        public async Task<IActionResult> GetAllTasks(Guid tableId)
        {
            var response = await _dataAccess.Projects.GetAllTasks(tableId);

            var mappedResponse = MapToDto<IEnumerable<TaskSimple>>(response);

            return Ok(mappedResponse);
        }

        [HttpGet("tasks/{taskId}")]
        public async Task<IActionResult> GetTaskById(Guid taskId)
        {
            var response = await _dataAccess.Projects.GetTaskById(taskId);

            var mappedResponse = MapToDto<TaskDetails>(response);

            return Ok(mappedResponse);
        }
    }
}
