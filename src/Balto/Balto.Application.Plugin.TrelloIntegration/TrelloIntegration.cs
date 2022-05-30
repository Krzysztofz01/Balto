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

                var project = PrepareProjectAggregate(table, currentUserId);
                await ProjectRepository.Add(project);

                if (_trelloIntegrationSettings.CreateTagsFromSquareBrackets)
                {
                    var tags = PrepareTagAggregates(table);
                    await TagRepository.Add(tags);
                }

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

        private Project PrepareProjectAggregate(TrelloTable table, Guid currentUserId)
        {
            var project = Project.Factory.Create(new ProjectEvents.V1.ProjectCreated
            {
                Title = table.Name,
                CurrentUserId = currentUserId
            });

            var tags = PrepareTagAggregates(table);

            var availableLists = table.Lists.Where(l => l is not null && !l.Closed);
            foreach (var list in availableLists)
            {
                project.Apply(new ProjectEvents.V1.ProjectTableCreated
                {
                    Id = project.Id,
                    Title = list.Name
                });

                var projectTableId = project.Tables.Single(t => t.Title == list.Name).Id;

                var listActions = table.Actions
                    .Where(a => a.Data is not null && a.Data.List is not null)
                    .Where(d => d.Data.List.Id == list.Id);

                foreach (var action in listActions)
                {
                    // TODO: Card null reference bug. Further investigation required
                    var card = action.Data.Card;
                    if (card is null) continue;

                    var cardName = (_trelloIntegrationSettings.CreateTagsFromSquareBrackets)
                        ? _tagBracketsRegex.Replace(card.Name, string.Empty).Trim()
                        : card.Name.Trim();

                    project.Apply(new ProjectEvents.V1.ProjectTaskCreated
                    {
                        Id = project.Id,
                        TableId = projectTableId,
                        CurrentUserId = currentUserId,
                        Title = cardName
                    });

                    var projectTask = project
                        .Tables.Single(t => t.Id == projectTableId)
                        .Tasks.Single(t => t.Title == cardName);

                    project.Apply(new ProjectEvents.V1.ProjectTaskUpdated
                    {
                        Id = project.Id,
                        TableId = projectTableId,
                        TaskId = projectTask.Id,
                        AssignedContributorId = projectTask.AssignedContributorId,
                        Content = card.Description,
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
                            CurrentUserId = currentUserId,
                            Status = true
                        });
                    }

                    if (_trelloIntegrationSettings.CreateTagsFromSquareBrackets)
                    {
                        var tagMatches = _tagBracketsRegex.Matches(card.Name)
                            .SelectMany(m => m.Groups.Values.Select(v => v.Value));
                        
                        foreach (var matchedTag in tagMatches)
                        {
                            var debugTags = tags.ToList();
                            var debugTagMatches = tagMatches.ToList();

                            var tag = tags.SingleOrDefault(t =>
                                t.Title == EscapeTagBraces(matchedTag));

                            if (tag is null) continue;

                            project.Apply(new ProjectEvents.V1.ProjectTaskTagAssigned
                            {
                                Id = project.Id,
                                TableId = projectTableId,
                                TaskId = projectTask.Id,
                                TagId = tag.Id
                            });
                        }
                    }
                }
            }
            return project;
        }

        private IEnumerable<Tag> PrepareTagAggregates(TrelloTable table)
        {
            var tags = table.Actions
                .Select(a => a.Data.Card)
                .Where(c => c is not null && !string.IsNullOrEmpty(c.Name))
                .SelectMany(c => _tagBracketsRegex.Matches(c.Name))
                .SelectMany(c => c.Groups.Values.Select(v => v.Value))
                .Distinct();

            var tagAggregates = new List<Tag>();
            foreach (var tag in tags)
            {
                tagAggregates.Add(Tag.Factory.Create(new TagEvents.V1.TagCreated
                {
                    Title = EscapeTagBraces(tag),
                    Color = _defaultTagColor
                }));
            }

            return tagAggregates;
        }

        private static string EscapeTagBraces(string tag)
        {
            var tagTrimmed = tag.Trim();
            return (tagTrimmed.Length > 2)
                ? tagTrimmed.Substring(1, tagTrimmed.Length - 2)
                : string.Empty;
        }
    }
}
