using Balto.Service.Handlers;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Balto.Service.Integration.Trello
{
    public class TrelloIntegrationService : ITrelloIntegrationService
    {
        public Task<IServiceResult> MigrateProject(IFormFile jsonFile, long userId)
        {
            throw new NotImplementedException();
        }
    }
}
