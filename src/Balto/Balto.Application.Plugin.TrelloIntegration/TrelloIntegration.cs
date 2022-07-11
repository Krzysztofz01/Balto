using Balto.Application.Plugin.Core;
using Balto.Application.Plugin.TrelloIntegration.Abstraction;
using Balto.Application.Plugin.TrelloIntegration.Models;
using Balto.Domain.Projects;
using ProjectEvents = Balto.Domain.Projects.Events;
using Balto.Domain.Tags;
using TagEvents = Balto.Domain.Tags.Events;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Balto.Application.Plugin.TrelloIntegration.Extensions;

namespace Balto.Application.Plugin.TrelloIntegration
{
    public class TrelloIntegration : BaltoGeneralPluginBase<TrelloIntegration>, ITrelloIntegration
    {
        private readonly TrelloIntegrationSettings _trelloIntegrationSettings;

        public TrelloIntegration(
            IUnitOfWork unitOfWork,
            ILogger<TrelloIntegration> logger,
            IOptions<TrelloIntegrationSettings> trelloIntegrationSettings) : base(unitOfWork, logger)
        {
            _trelloIntegrationSettings = trelloIntegrationSettings.Value ??
                throw new ArgumentNullException(nameof(trelloIntegrationSettings));
        }

        protected override string PluginName => "Trello integration for Balto platform.";
        protected override string PluginDescription => "Plugin that allows to import Trello boards as Balto projects.";
        protected override string PluginVersion => "0.1.0";

        private const int _maxFileSize = 52428800;
        private readonly Regex _tagBracketsRegex = new(@"\[[^][]*]");

        private const string _defaultTagColor = "#0079bf";

        public async Task ImportTable(IFormFile jsonFile, Guid currentUserId)
        {
            try
            {
                if (!_trelloIntegrationSettings.Enabled)
                    throw new PluginException("Pugin disabled");

                if (!IsFileValid(jsonFile))
                    throw new PluginException("The uploaded file is invalid.");

                var table = await DeserializeTrelloTable(jsonFile);

                IEnumerable<Tag> tags = null;
                if (_trelloIntegrationSettings.CreateTagsFromSquareBrackets)
                {
                    tags = ParseTrelloTableToBaltoTags(table);
                    await TagRepository.AddRange(tags);
                }

                var project = ParseTrelloTableToBaltoProject(table, currentUserId, tags);
                await ProjectRepository.Add(project);

                await CommitChanges();
            }
            catch (Exception ex)
            {
                throw new PluginException(ex.Message);
            }
        }

        private static async Task<TrelloTable> DeserializeTrelloTable(IFormFile jsonFile)
        {
            var textJsonContentBuilder = new StringBuilder();

            using var streamReader = new StreamReader(jsonFile.OpenReadStream());
            while (streamReader.Peek() >= 0) textJsonContentBuilder.AppendLine(await streamReader.ReadLineAsync());

            var textJsonContent = textJsonContentBuilder.ToString();
            if (textJsonContent is null || textJsonContent.Length == 0)
                throw new PluginException("Failed to process the uploaded file.");

            var deserializedTable = JsonSerializer.Deserialize<TrelloTable>(textJsonContent, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return deserializedTable;
        }

        private static bool IsFileValid(IFormFile jsonFile)
        {
            if (jsonFile is null) return false;

            if (jsonFile.Length > _maxFileSize) return false;

            return true;
        }

        private static Project ParseTrelloTableToBaltoProject(TrelloTable table, Guid boardOwnerId, IEnumerable<Tag> tags = null)
        {
            var project = Project.Factory.Create(new ProjectEvents.V1.ProjectCreated
            {
                Title = table.Name,
                CurrentUserId = boardOwnerId
            });

            var availableLists = table.Lists
                .Where(l => l is not null)
                .Where(l => l.IsValid())
                .Where(l => !l.Closed);

            foreach (var list in availableLists)
            {
                project.Apply(new ProjectEvents.V1.ProjectTableCreated
                {
                    Id = project.Id,
                    Title = list.Name
                });

                var projectTableId = project
                    .Tables.Last().Id;

                var availableListActions = table.Actions
                    .Where(a => a is not null)
                    .Where(a => a.IsValid())
                    .Where(a => a.Data.IsValid())
                    .Where(a => a.Data.List.IsValid())
                    .Where(a => a.Data.List.Id == list.Id);

                foreach (var action in availableListActions)
                {
                    var card = action.Data.Card;
                    if (!card.IsValid()) continue;

                    var cardName = card.Name.Trim();

                    var cardDescription = card.Description ?? string.Empty;

                    project.Apply(new ProjectEvents.V1.ProjectTaskCreated
                    {
                        Id = project.Id,
                        TableId = projectTableId,
                        CurrentUserId = boardOwnerId,
                        Title = cardName
                    });

                    var projectTask = project
                        .Tables.Single(t => t.Id == projectTableId)
                        .Tasks.Last();

                    project.Apply(new ProjectEvents.V1.ProjectTaskUpdated
                    {
                        Id = project.Id,
                        TableId = projectTableId,
                        TaskId = projectTask.Id,
                        AssignedContributorId = projectTask.AssignedContributorId,
                        Content = cardDescription,
                        Deadline = projectTask.Deadline,
                        StartingDate = projectTask.StartingDate,
                        Priority = projectTask.Priority,
                        Title = projectTask.Title
                    });

                    if (card.Closed)
                    {
                        project.Apply(new ProjectEvents.V1.ProjectTaskStatusChanged
                        {
                            Id = project.Id,
                            TableId = projectTableId,
                            TaskId = projectTask.Id,
                            CurrentUserId = boardOwnerId,
                            Status = true
                        });
                    }

                    //TODO: Implement support the tags
                    throw new NotImplementedException();
                }
            }

            return project;
        }


        //TODO: Implement the system of extracting the tags from titles
        private static IEnumerable<Tag> ParseTrelloTableToBaltoTags(TrelloTable table)
        {
            throw new NotImplementedException();

            var cardNameLabelMaps = table.Actions
                    .Where(a => a is not null)
                    .Where(a => a.IsValid())
                    .Where(a => a.Data.IsValid())
                    .Where(a => a.Data.Card.IsValid())
                    .Select(a => a.Data.Card)
                    .Select(c => new { c.Name, Label = GetLabelRepresentation(c.Labels) });

            var tagAggregates = new List<Tag>();
            foreach (var nameLableMap in cardNameLabelMaps)
            {

            }

            return tagAggregates;
        }

        private static string GetLabelRepresentation(IEnumerable<TrelloLabel> trelloLabels)
        {
            if (trelloLabels is null) return null;

            var validLabels = trelloLabels.Where(l => l.IsValid());

            return validLabels.Any() ? validLabels.First().ColorName : null;
        }
    }
}
