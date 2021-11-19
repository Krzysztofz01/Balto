using AutoMapper;
using Balto.API.Utility;
using Balto.Application.Projects;
using Balto.Domain.Projects;
using Balto.Domain.Projects.ProjectTables;
using Balto.Domain.Projects.ProjectTasks;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Balto.Application.Projects.Dto;

namespace Balto.API.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/project")]
    [ApiVersion("1.0")]
    [Authorize]
    public class ProjectQueryController : ControllerBase
    {
        private readonly IDataAccess _dataAccess;
        private readonly IMapper _mapper;

        public ProjectQueryController(IDataAccess dataAccess, IMapper mapper)
        {
            _dataAccess = dataAccess ??
                throw new ArgumentNullException(nameof(dataAccess));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProjects() =>
            await RequestHandler.MapQuery<IEnumerable<Project>, IEnumerable<ProjectSimple>>(_dataAccess.Projects.GetAllProjects(), _mapper);

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetProjectById(Guid projectId) =>
            await RequestHandler.MapQuery<Project, ProjectDetails>(_dataAccess.Projects.GetProjectById(projectId), _mapper);

        [HttpGet("{projectId}/tables")]
        public async Task<IActionResult> GetAllTables(Guid projectId) =>
            await RequestHandler.MapQuery<IEnumerable<ProjectTable>, IEnumerable<TableSimple>>(_dataAccess.Projects.GetAllTables(projectId), _mapper);

        [HttpGet("tables/{tableId}")]
        public async Task<IActionResult> GetTableById(Guid tableId) =>
            await RequestHandler.MapQuery<ProjectTable, TableDetails>(_dataAccess.Projects.GetTableById(tableId), _mapper);

        [HttpGet("tables/{tableId}/tasks")]
        public async Task<IActionResult> GetAllTasks(Guid tableId) =>
            await RequestHandler.MapQuery<IEnumerable<ProjectTask>, IEnumerable<TaskSimple>>(_dataAccess.Projects.GetAllTasks(tableId), _mapper);

        [HttpGet("tasks/{taskId}")]
        public async Task<IActionResult> GetTaskById(Guid taskId) =>
            await RequestHandler.MapQuery<ProjectTask, TaskDetails>(_dataAccess.Projects.GetTaskById(taskId), _mapper);
    }
}
