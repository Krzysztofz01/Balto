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
        private const string _tagBracketsRegexPattern = @"\[[^][]*]";

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

                if (_trelloIntegrationSettings.CreateTagsFromSquareBrackets)
                {
                    var tags = PrepareTagAggregates(table);

                    ApplyPreparedTags(table, project, tags);
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

        private static Project PrepareProjectAggregate(TrelloTable table, Guid currentUserId)
        {
            var project = Project.Factory.Create(new ProjectEvents.V1.ProjectCreated
            {
                Title = table.Name,
                CurrentUserId = currentUserId
            });

            var availableLists = table.Lists.Where(l => !l.Closed);
            foreach (var list in availableLists)
            {
                project.Apply(new ProjectEvents.V1.ProjectTableCreated
                {
                    Id = project.Id,
                    Title = list.Name
                });

                var projectTableId = project.Tables.Single(t => t.Title == list.Name).Id;

                var listActions = table.Actions.Where(a => a.Data.List.Id == list.Id);
                foreach (var action in listActions)
                {
                    // TODO: Card null reference bug. Further investigation required
                    var card = action.Data.Card;
                    if (card is null) continue;

                    project.Apply(new ProjectEvents.V1.ProjectTaskCreated
                    {
                        Id = project.Id,
                        TableId = projectTableId,
                        CurrentUserId = currentUserId,
                        Title = card.Name
                    });

                    var projectTask = project
                        .Tables.Single(t => t.Id == projectTableId)
                        .Tasks.Single(t => t.Title == card.Name);

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
                }
            }

            return project;
        }


        private static IEnumerable<Tag> PrepareTagAggregates(TrelloTable table)
        {
            var bracketsRegex = new Regex(_tagBracketsRegexPattern);

            var cardTitleLabelColorMap = table.Actions
                .Select(a => a.Data.Card)
                .Select(c => new
                {
                    Title = c.Name,
                    ColorName = c.Labels.FirstOrDefault().ColorName
                })
                .Select(c => new
                {
                    Titles = bracketsRegex.Matches(c.Title).SelectMany(m => m.Groups.Values.Select(v => v.Value)),
                    Color = GetMatchingTrelloColor(c.ColorName)
                });

            var tagAggregates = new List<Tag>();
            foreach (var titlesColorMap in cardTitleLabelColorMap)
            {
                var tagTitles = titlesColorMap.Titles
                    .Distinct()
                    .Select(c => c.Trim().Skip(1).SkipLast(1).ToString());

                foreach (var tagTitle in tagTitles)
                {
                    if (tagAggregates.Any(t => t.Title == tagTitle)) continue;

                    tagAggregates.Add(Tag.Factory.Create(new TagEvents.V1.TagCreated
                    {
                        Title = tagTitle,
                        Color = titlesColorMap.Color
                    }));
                }
            }

            return tagAggregates;
        }

        private void ApplyPreparedTags(TrelloTable table, Project project, IEnumerable<Tag> tags)
        {
            throw new NotImplementedException();
        }

        private static string GetMatchingTrelloColor(string colorName)
        {
            return colorName switch
            {
                "green" => "#61bd4f",
                "yellow" => "#f2d600",
                "orange" => "#ff9f1a",
                "red" => "#eb5a46",
                "purple" => "#c377e0",
                "blue" => "#0079bf",
                _ => "#ffffff",
            };
        }
    }
}
