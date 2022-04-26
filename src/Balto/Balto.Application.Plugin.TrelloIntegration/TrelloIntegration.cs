using Balto.Application.Plugin.Core;
using Balto.Application.Plugin.TrelloIntegration.Abstraction;
using Balto.Application.Plugin.TrelloIntegration.Models;
using Balto.Domain.Projects;
using Balto.Domain.Tags;
using Balto.Infrastructure.Core.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
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

        private async Task<TrelloTable> DeserializeTrelloTable(IFormFile jsonFile)
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

        private bool IsFileValid(IFormFile jsonFile)
        {
            throw new NotImplementedException();
        }

        private Project PrepareProjectAggregate(TrelloTable table, Guid currentUserId)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<Tag> PrepareTagAggregates(TrelloTable table)
        {
            throw new NotImplementedException();
        }

        private void ApplyPreparedTags(TrelloTable table, Project project, IEnumerable<Tag> tags)
        {
            throw new NotImplementedException();
        }
    }
}
