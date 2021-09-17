using Balto.Application.Integrations.Trello.Models;
using Balto.Domain.Aggregates.Project;
using System;
using System.Linq;
using System.Text.Json;

namespace Balto.Application.Integrations.Trello
{
    public class TrelloIntegration : ITrelloIntegration
    {
        public Board DeserializeExportFile(string trelloJson)
        {
            if (!string.IsNullOrEmpty(trelloJson))
                throw new ArgumentNullException(nameof(trelloJson));

            return JsonSerializer.Deserialize<Board>(trelloJson);
        }

        public Project GenerateProject(Board board, Guid currentUserId)
        {
            var project = Project.Factory.Create(new ProjectOwnerId(currentUserId), ProjectTitle.FromString(board.Name));

            var availableLists = board.Lists.Where(l => !l.Closed);
            foreach(var list in availableLists)
            {
                var currentListActions = board.Actions.Where(a => a.Data.List.Id == list.Id);

                project.AddTable(list.Name, currentUserId);
                var tableId = project.Tables.Last().Id.Value;

                foreach(var action in currentListActions)
                {
                    if (action.Data.Card is null) continue;

                    project.AddCard(tableId, action.Data.Card.Name, currentUserId);
                    var card = project.Tables.Single(e => e.Id.Value == tableId).Cards.Last();

                    project.UpdateCard(
                        card.Id.Value,
                        card.Title.Value,
                        action.Data.Card.Description,
                        card.Color.Value, action.Date,
                        false,
                        card.Deadline.Date,
                        card.Deadline.UserId,
                        card.Priority.Value);

                    if (action.Data.Card.Closed) project.ChangeCardStatus(card.Id.Value, currentUserId);
                }
            }

            return project;
        }
    }
}
