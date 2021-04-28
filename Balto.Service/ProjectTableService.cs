using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public class ProjectTableService : IProjectTableService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IProjectTableRepository projectTableRepository;
        private readonly IMapper mapper;

        public ProjectTableService(
            IProjectRepository projectRepository,
            IProjectTableRepository projectTableRepository,
            IMapper mapper)
        {
            this.projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));

            this.projectTableRepository = projectTableRepository ??
                throw new ArgumentNullException(nameof(projectTableRepository));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IServiceResult> Add(ProjectTableDto projectTable, long projectId, long userId)
        {
            //Check if given user owns a project with given id
            if (await projectRepository.SingleUsersProject(projectId, userId) is null) return new ServiceResult(ResultStatus.NotPermited);

            //Map DTO to domain model and assign the foreign key to point to given project
            var projectTableMapped = mapper.Map<ProjectTable>(projectTable); 
            projectTableMapped.ProjectId = projectId;

            await projectTableRepository.Add(projectTableMapped);

            if (await projectTableRepository.Save() > 0)
            {
                return new ServiceResult(ResultStatus.Sucess);
            }
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<IServiceResult> Delete(long projectId, long projectTableId, long userId)
        {
            var projectTable = await projectTableRepository.SingleUsersTable(projectId, projectTableId, userId);
            if(projectTable is null) return new ServiceResult(ResultStatus.NotFound);

            projectTableRepository.Remove(projectTable);
            if(await projectTableRepository.Save() > 0)
            {
                return new ServiceResult(ResultStatus.Sucess);
            }
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<ServiceResult<ProjectTableDto>> Get(long projectId, long projectTableId, long userId)
        {
            var projectTable = await projectTableRepository.SingleUsersTable(projectId, projectTableId, userId);
            if (projectTable is null) return new ServiceResult<ProjectTableDto>(ResultStatus.NotFound);

            return new ServiceResult<ProjectTableDto>(mapper.Map<ProjectTableDto>(projectTable));
        }

        public async Task<ServiceResult<IEnumerable<ProjectTableDto>>> GetAll(long projectId, long userId)
        {
            var projectsTabels = projectTableRepository.AllUserTabels(projectId, userId);
            if (projectsTabels is null) return new ServiceResult<IEnumerable<ProjectTableDto>>(ResultStatus.NotFound);

            return new ServiceResult<IEnumerable<ProjectTableDto>>(mapper.Map<IEnumerable<ProjectTableDto>>(projectsTabels));
        }

        public async Task<IServiceResult> Update(ProjectTableDto projectTable, long projectId, long projectTableId, long userId)
        {
            //Possible changes: name
            var projectTableBase = await projectTableRepository.SingleUsersTable(projectId, projectTableId, userId);
            if (projectTableBase is null) return new ServiceResult(ResultStatus.NotFound);

            bool changes = true;

            if (projectTableBase.Name != projectTable.Name && projectTable.Name != null)
            {
                changes = true;
                projectTableBase.Name = projectTable.Name;
            }

            if (changes)
            {
                if (await projectTableRepository.Save() > 0)
                {
                    return new ServiceResult(ResultStatus.Sucess);
                }
                return new ServiceResult(ResultStatus.Failed);
            }

            return new ServiceResult(ResultStatus.Sucess);
        }
    }
}
