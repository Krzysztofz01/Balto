using AutoMapper;
using Balto.Application.Aggregates.Project;
using Balto.Infrastructure.SqlServer.Context;
using Balto.Web.Handlers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ProjectDomain = Balto.Domain.Aggregates.Project;
using static Balto.Application.Aggregates.Project.Dto.V1;

namespace Balto.Web.Controllers.Project
{
    [ApiController]
    [Route("api/v{version:apiVersion}/project")]
    [ApiVersion("1.0")]
    [Authorize]
    public class QueryController : ControllerBase
    {
        private readonly DbSet<ProjectDomain.Project> _projects;
        private readonly IMapper _mapper;

        public QueryController(
            BaltoDbContext dbContext,
            IMapper mapper)
        {
            _projects = dbContext.Set<ProjectDomain.Project>() ??
                throw new ArgumentNullException(nameof(dbContext));

            _mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() =>
            await RequestHandler.HandleMappedQuery<IEnumerable<ProjectDomain.Project>, IEnumerable<ProjectSimple>>(_projects.GetAllProjects, _mapper);

        [HttpGet("{projectId}")]
        public async Task<IActionResult> GetById(Guid projectId) =>
            await RequestHandler.HandleMappedQuery<Guid, ProjectDomain.Project, ProjectDetails>(projectId, _projects.GetProjectById, _mapper);

        [HttpGet("card/{cardId}")]
        public async Task<IActionResult> GetCardById(Guid cardId) =>
            await RequestHandler.HandleMappedQuery<Guid, ProjectDomain.Card.ProjectTableCard, CardDetails>(cardId, _projects.GetCardById, _mapper);
    }
}
