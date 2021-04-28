using Balto.Service.Handlers;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Balto.Service.Integration.Trello
{
    public interface ITrelloIntegrationService
    {
        Task<IServiceResult> MigrateProject(IFormFile jsonFile, long userId);
    }
}
