using AutoMapper;
using Balto.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;

namespace Balto.Web.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/project")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService projectService;
        private readonly IProjectTableService projectTableService;
        private readonly IProjectTableEntryService projectTableEntryService;
        private readonly IMapper mapper;
        private readonly ILogger<ProjectController> logger;

        public ProjectController(
            IProjectService projectService,
            IProjectTableService projectTableService,
            IProjectTableEntryService projectTableEntryService,
            IMapper mapper,
            ILogger<ProjectController> logger)
        {
            this.projectService = projectService ??
                throw new ArgumentNullException(nameof(projectService));

            this.projectTableService = projectTableService ??
                throw new ArgumentNullException(nameof(projectTableService));

            this.projectTableEntryService = projectTableEntryService ??
                throw new ArgumentNullException(nameof(projectTableEntryService));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));

            this.logger = logger ??
                throw new ArgumentNullException(nameof(logger));
        }
        

    }
}
