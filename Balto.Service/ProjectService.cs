﻿using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Service
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IMapper mapper;

        public ProjectService(
            IProjectRepository projectRepository,
            IMapper mapper)
        {
            this.projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Add(ProjectDto project)
        {
            await projectRepository.Add(mapper.Map<Project>(project));

            if (await projectRepository.Save() > 0) return true;
            return false;
        }

        public async Task<bool> Delete(long projectId, long userId)
        {
            var project = await projectRepository.SingleUsersProject(projectId, userId);

            if(project != null)
            {
                projectRepository.Remove(project);
                if (await projectRepository.Save() > 0) return true;
            }
            return false;
        }

        public async Task<ProjectDto> Get(long projectId, long userId)
        {
            //Get users with given id project with given id
            var project = await projectRepository.SingleUsersProject(projectId, userId);

            if(project != null)
            {
                return mapper.Map<ProjectDto>(project);
            }
            return null;
        }

        public async Task<IEnumerable<ProjectDto>> GetAll(long userId)
        {
            //Get all projects for user with given id
            var projects = projectRepository.Find(p => p.OwnerId == userId);

            //Map values from domain models to service dto
            return mapper.Map<IEnumerable<ProjectDto>>(projects);
        }

        public async Task<bool> Update(ProjectDto project)
        {
            //Possible changes: name
            var projectBase = await projectRepository.SingleUsersProject(project.Id, project.OwnerId);

            if(project != null)
            {
                bool changes = false;

                if (projectBase.Name != project.Name)
                {
                    changes = true;
                    projectBase.Name = project.Name;
                }

                if(changes)
                {
                    projectRepository.UpdateState(projectBase);
                    if (await projectRepository.Save() > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
