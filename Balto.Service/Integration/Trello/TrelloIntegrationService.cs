using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Dto;
using Balto.Service.Handlers;
using Balto.Service.Integration.Trello.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Balto.Service.Integration.Trello
{
    public class TrelloIntegrationService : ITrelloIntegrationService
    {
        private readonly IProjectRepository projectRepository;
        private readonly IProjectTableRepository projectTableRepository;
        private readonly IProjectTableEntryRepository projectTableEntryRepository;
        private readonly IMapper mapper;

        public TrelloIntegrationService(
            IProjectRepository projectRepository,
            IProjectTableRepository projectTableRepository,
            IProjectTableEntryRepository projectTableEntryRepository,
            IMapper mapper)
        {
            this.projectRepository = projectRepository ??
                throw new ArgumentNullException(nameof(projectRepository));

            this.projectTableRepository = projectTableRepository ??
                throw new ArgumentNullException(nameof(projectTableRepository));

            this.projectTableEntryRepository = projectTableEntryRepository ??
                throw new ArgumentNullException(nameof(projectTableEntryRepository));

            this.mapper = mapper ??
                throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IServiceResult> MigrateProject(IFormFile jsonFile, long userId)
        {
            var result = new StringBuilder();

            using (var reader = new StreamReader(jsonFile.OpenReadStream()))
            {
                while (reader.Peek() >= 0) result.AppendLine(await reader.ReadLineAsync());
            }

            var resultString = result.ToString();
            if (resultString is null && resultString.Length == 0) return new ServiceResult(ResultStatus.Failed);

            var table = JsonConvert.DeserializeObject<Table>(resultString);
            if(table is null) return new ServiceResult(ResultStatus.Failed);

            return await GenerateStructureV1(table, userId);
        }

        private async Task<IServiceResult> GenerateStructureV1(Table table, long userId)
        {
            //Generate new project
            var project = new ProjectDto
            {
                Name = table.Name,
                OwnerId = userId
            };

            var projectMapped = mapper.Map<Project>(project);
            
            await projectRepository.Add(projectMapped);
            if (await projectRepository.Save() == 0) return new ServiceResult(ResultStatus.Failed);

            long projectId = projectMapped.Id;

            //Add all tables(lists)
            foreach(var list in table.Lists)
            {
                if (!list.Closed)
                {
                    var projectTable = new ProjectTableDto
                    {
                        Name = list.Name
                    };

                    var projectTableMapped = mapper.Map<ProjectTable>(projectTable);
                    projectTableMapped.ProjectId = projectId;

                    await projectTableRepository.Add(projectTableMapped);
                }
            }

            if(await projectTableRepository.Save() == 0) return new ServiceResult(ResultStatus.Failed);

            //Add all entries(cards)
            foreach(var card in table.Actions)
            {
                if (card.Data.List != null && card.Data.Card != null)
                {
                    var tableForEntry = await projectTableRepository.SingleOrDefault(x => x.Name == card.Data.List.Name && x.ProjectId == projectId);
                    if (tableForEntry is null) continue;

                    var entry = new ProjectTableEntryDto
                    {
                        Name = card.Data.Card.Name,
                        Content = card.Data.Card.Description,
                        Finished = (card.Data.Card.Closed) ? true : false
                    };

                    var entryMapped = mapper.Map<ProjectTableEntry>(entry);
                    entryMapped.ProjectTableId = tableForEntry.Id;

                    await projectTableEntryRepository.Add(entryMapped);
                }
            }

            if (await projectTableEntryRepository.Save() == 0) return new ServiceResult(ResultStatus.Failed);

            return new ServiceResult(ResultStatus.Sucess);
        }
    }
}
