using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IProjectReadWriteUserRepository projectReadWriteUserRepository;
        private readonly IUserService userService;
        private readonly IMapper mapper;

        public ProjectService(
            IProjectRepository projectRepository,
            IProjectReadWriteUserRepository projectReadWriteUserRepository,
            IUserService userService,
            IMapper mapper)
        {
            this.projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));

            this.projectReadWriteUserRepository = projectReadWriteUserRepository ??
                throw new ArgumentNullException(nameof(projectReadWriteUserRepository));

            this.userService = userService ??
                throw new ArgumentNullException(nameof(userService));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IServiceResult> Add(ProjectDto project, long userId)
        {
            var projectMapped = mapper.Map<Project>(project);
            projectMapped.OwnerId = userId;

            await projectRepository.Add(projectMapped);

            if (await projectRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<IServiceResult> Delete(long projectId, long userId)
        {
            var project = await projectRepository.SingleUsersProjectOwner(projectId, userId);
            if (project is null) return new ServiceResult(ResultStatus.NotFound);

            projectRepository.Remove(project);
            
            if (await projectRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<ServiceResult<ProjectDto>> Get(long projectId, long userId)
        {
            //Get users with given id project with given id
            var project = await projectRepository.SingleUsersProject(projectId, userId);
            if (project is null) return new ServiceResult<ProjectDto>(ResultStatus.NotFound);

            return new ServiceResult<ProjectDto>(mapper.Map<ProjectDto>(project));
        }

        public async Task<ServiceResult<IEnumerable<ProjectDto>>> GetAll(long userId)
        {
            //Get all projects for user with given id
            var projects = projectRepository.AllUsersProjects(userId).ToList();

            //Map values from domain models to service dto
            return new ServiceResult<IEnumerable<ProjectDto>>(mapper.Map<IEnumerable<ProjectDto>>(projects));
        }

        public async Task<IServiceResult> InviteUser(long projectId, string collaboratorEmail, long userId)
        {
            //Check if the user who requested the invitation is the owner
            //Only owners have permission to invite collaborators
            if (!await projectRepository.IsOwner(projectId, userId)) return new ServiceResult(ResultStatus.NotPermited);

            var collaboratorId = await userService.GetIdByEmail(collaboratorEmail);
            if (collaboratorId is null) return new ServiceResult(ResultStatus.NotFound);

            if (await projectRepository.IsOwner(projectId, (long)collaboratorId)) return new ServiceResult(ResultStatus.NotPermited);

            await projectReadWriteUserRepository.AddCollaborator(projectId, (long)collaboratorId);
            if (await projectReadWriteUserRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
            return new ServiceResult(ResultStatus.Failed);
        }

        public async Task<IServiceResult> Update(ProjectDto project, long projectId, long userId)
        {
            //Possible changes: name
            var projectBase = await projectRepository.SingleUsersProject(projectId, userId);
            if(projectBase is null) return new ServiceResult(ResultStatus.NotFound);

            bool changes = false;

            if (projectBase.Name != project.Name && project.Name != null)
            {
                changes = true;
                projectBase.Name = project.Name;
            }

            if (changes)
            {
                projectRepository.UpdateState(projectBase);
                if (await projectRepository.Save() > 0)
                {
                    return new ServiceResult(ResultStatus.Sucess);
                }
                return new ServiceResult(ResultStatus.Failed);
            }
            return new ServiceResult(ResultStatus.Sucess);
        }
    }
}
