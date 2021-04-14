using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    class ProjectTableService : IProjectTableService
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

        public async Task<bool> Add(ProjectTableDto projectTable, long projectId, long userId)
        {
            //Check if given user owns a project with given id
            if (await projectRepository.SingleUsersProject(projectId, userId) is null) return false;

            //Map DTO to domain model and assign the foreign key to point to given project
            var projectTableMapped = mapper.Map<ProjectTable>(projectTable); 
            projectTableMapped.ProjectId = projectId;

            await projectTableRepository.Add(projectTableMapped);

            if (await projectTableRepository.Save() > 0)
            {
                return true;
            }
            return false;
        }

        public Task<bool> Delete(long projectTableId, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<ProjectTableDto> Get(long projectTableId, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProjectTableDto>> GetAll(long projectId, long userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Update(ProjectTableDto projectTable, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
