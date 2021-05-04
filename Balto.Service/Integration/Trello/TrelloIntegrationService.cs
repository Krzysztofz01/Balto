using AutoMapper;
using Balto.Domain;
using Balto.Repository;
using Balto.Service.Handlers;
using Balto.Service.Integration.Trello.Models;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            return await GenerateStructureV2(table, userId);
        }

        private async Task<IServiceResult> GenerateStructureV2(Table table, long userId)
        {
            //Generate project object
            var project = new Project
            {
                Name = table.Name,
                OwnerId = userId,
                Tabels = new List<ProjectTable>()
            };

            //Generate tabele objects
            foreach(var list in table.Lists)
            {
                if(!list.Closed)
                {
                    project.Tabels.Add(new ProjectTable
                    {
                        Name = list.Name,
                        Entries = new List<ProjectTableEntry>()
                    }); 
                }
            }

            //Generate entry objects and assing it to the corresponding tables
            foreach(var action in table.Actions)
            {
                if (action.Data.List is null || action.Data.Card is null) continue;

                var tableReference = project.Tabels.SingleOrDefault(t => t.Name == action.Data.List.Name);
                if (tableReference is null) continue;

                tableReference.Entries.Add(new ProjectTableEntry
                {
                    Name = action.Data.Card.Name,
                    Content = action.Data.Card.Description,
                    Finished = (action.Data.Card.Closed) ? true : false,
                    UserAddedId = userId
                });
            }

            await projectRepository.Add(project);

            if (await projectRepository.Save() > 0) return new ServiceResult(ResultStatus.Sucess);
            return new ServiceResult(ResultStatus.Failed);
        }
    }
}
