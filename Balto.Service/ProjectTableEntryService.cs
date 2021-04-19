using AutoMapper;
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

        public async Task<bool> Add(ProjectTableEntryDto projectTableEntry, long projectTableId, long userId)
        {
            //Check if user owns a project with given projectTable
            var projectTable = await projectTableRepository.SingleOrDefault(p => p.Project.OwnerId == userId && p.Id == projectTableId);
            if (projectTable is null) return false;

            //Set the order of the new entry
            projectTableEntry.Order = projectTableEntryRepository.GetEntryOrder(projectTableId);

            //Map DTO to domain model
            var mappedEntry = mapper.Map<ProjectTableEntry>(projectTableEntry);

            await projectTableEntryRepository.Add(mappedEntry);
            if(await projectTableEntryRepository.Save() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<bool> ChangeOrder(IEnumerable<long> entryIds, long projectTableId, long userId)
        {
            //Every entry has a unique integer value which indicates the order
            //we input an array of entry Ids in specific order and than we change
            //the order property to the coresponding value.

            var entries = projectTableEntryRepository.Find(p => p.ProjectTableId == projectTableId && p.ProjectTable.Project.OwnerId == userId).ToList();
            if (entries is null) return false;


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

        public async Task<bool> ChangeState(long projectTableEntryId, bool state, long userId)
        {
            //Change state of task (finished / pending)

            var entry = await projectTableEntryRepository.SingleOrDefault(p => p.Id == projectTableEntryId && p.ProjectTable.Project.OwnerId == userId);
            if (entry is null) return false;

            if (entry.Finished != state)
            {
                entry.Finished = state;

                projectTableEntryRepository.UpdateState(entry);
                if (await projectTableEntryRepository.Save() > 0) return true;
            }
            return false;
        }

        public async Task<bool> Delete(long projectTableEntryId, long userId)
        {
            var entry = await projectTableEntryRepository.SingleOrDefault(e => e.Id == projectTableEntryId && e.ProjectTable.Project.OwnerId == userId);
            if (entry is null) return false;

            projectTableEntryRepository.Remove(entry);
            if(await projectTableEntryRepository.Save() > 0)
            {
                return true;
            }
            return false;
        }

        public async Task<ProjectTableEntryDto> Get(long projectTableEntryId, long userId)
        {
            var entry = await projectTableEntryRepository.SingleOrDefault(e => e.Id == projectTableEntryId && e.ProjectTable.Project.OwnerId == userId);
            return mapper.Map<ProjectTableEntryDto>(entry);
        }

        public async Task<IEnumerable<ProjectTableEntryDto>> GetAll(long projectTableId, long userId)
        {
            var entries = projectTableEntryRepository.Find(e => e.ProjectTableId == projectTableId && e.ProjectTable.Project.OwnerId == userId);

            return mapper.Map<IEnumerable<ProjectTableEntryDto>>(entries);
        }
    }
}
