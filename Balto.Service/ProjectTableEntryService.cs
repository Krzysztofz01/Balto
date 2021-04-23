﻿using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Service
{
    public class ProjectTableEntryService : IProjectTableEntryService
    {
        private readonly IProjectTableRepository projectTableRepository;
        private readonly IProjectTableEntryRepository projectTableEntryRepository;
        private readonly IMapper mapper;

        public ProjectTableEntryService(
            IProjectTableRepository projectTableRepository,
            IProjectTableEntryRepository projectTableEntryRepository,
            IMapper mapper)
        {
            this.projectTableRepository = projectTableRepository ??
                throw new ArgumentNullException(nameof(projectTableRepository));

            this.projectTableEntryRepository = projectTableEntryRepository ??
                throw new ArgumentNullException(nameof(projectTableEntryRepository));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<bool> Add(ProjectTableEntryDto projectTableEntry, long projectId, long projectTableId, long userId)
        {
            //Check if user owns a project with given projectTable
            var projectTable = await projectTableRepository.SingleUsersTable(projectId, projectTableId, userId);
            if (projectTable is null) return false;

            //Set the order of the new entry
            projectTableEntry.Order = await projectTableEntryRepository.GetEntryOrder(projectTableId);

            //Map DTO to domain model
            var mappedEntry = mapper.Map<ProjectTableEntry>(projectTableEntry);
            mappedEntry.ProjectTableId = projectTableId;

            await projectTableEntryRepository.Add(mappedEntry);
            if(await projectTableEntryRepository.Save() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeOrder(IEnumerable<long> entryIds, long projectId, long projectTableId, long userId)
        {
            //Every entry has a unique integer value which indicates the order
            //we input an array of entry Ids in specific order and than we change
            //the order property to the coresponding value.

            var entries = projectTableEntryRepository.AllUsersEntries(projectId, projectTableId, userId);
            if (!entries.Any()) return false;

            if (entries.Count() != entryIds.Count()) return false;


            long index = 0;
            foreach(var id in entryIds)
            {
                var entry = entries.SingleOrDefault(e => e.Id == id);
                if (entry is null) continue;

                entry.Order = index;
                index++;
                projectTableEntryRepository.UpdateState(entry);
            }

            if(await projectTableEntryRepository.Save() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeState(long projectId, long projectTableId, long projectTableEntryId, long userId)
        {
            //Change state of task (finished / pending)

            var entry = await projectTableEntryRepository.SingleUsersEntry(projectId, projectTableId, projectTableEntryId, userId);
            if (entry != null)
            {
                entry.Finished = !entry.Finished;

                if (await projectTableEntryRepository.Save() > 0) return true;
            }
            return false;
        }

        public async Task<bool> Delete(long projectId, long projectTableId, long projectTableEntryId, long userId)
        {
            var entry = await projectTableEntryRepository.SingleUsersEntry(projectId, projectTableId, projectTableEntryId, userId);
            if (entry is null) return false;

            projectTableEntryRepository.Remove(entry);
            if(await projectTableEntryRepository.Save() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<ProjectTableEntryDto> Get(long projectId, long projectTableId, long projectTableEntryId, long userId)
        {
            var entry = await projectTableEntryRepository.SingleUsersEntry(projectId, projectTableId, projectTableEntryId, userId);
            return mapper.Map<ProjectTableEntryDto>(entry);
        }

        public async Task<IEnumerable<ProjectTableEntryDto>> GetAll(long projectId, long projectTableId, long userId)
        {
            var entries = projectTableEntryRepository.AllUsersEntries(projectId, projectTableId, userId);

            return mapper.Map<IEnumerable<ProjectTableEntryDto>>(entries);
        }

        public async Task<bool> Update(ProjectTableEntryDto projectTableEntry, long projectId, long projectTableId, long projectTableEntryId, long userId)
        {
            //Possible changes: name, content
            var entry = await projectTableEntryRepository.SingleUsersEntry(projectId, projectTableId, projectTableEntryId, userId);

            if(entry != null)
            {
                bool changes = false;

                if(entry.Name != projectTableEntry.Name && projectTableEntry.Name != null)
                {
                    entry.Name = projectTableEntry.Name;
                    changes = true;
                }

                if(entry.Content != projectTableEntry.Content && projectTableEntry.Content != null)
                {
                    entry.Content = projectTableEntry.Content;
                    changes = true;
                }

                if(changes)
                {
                    if (await projectTableEntryRepository.Save() > 0) return true;
                }
            }
            return false;
        }
    }
}
