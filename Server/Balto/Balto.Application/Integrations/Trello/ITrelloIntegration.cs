using Balto.Application.Integrations.Trello.Models;
using Balto.Domain.Aggregates.Project;
using System;

namespace Balto.Application.Integrations.Trello
{
    public interface ITrelloIntegration
    {
        Board DeserializeExportFile(string trelloJson);
        Project GenerateProject(Board board, Guid currentUserId);
    }
}
