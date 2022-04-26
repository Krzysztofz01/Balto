using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Balto.Application.Plugin.TrelloIntegration.Abstraction
{
    public interface ITrelloIntegration
    {
        Task ImportTable(IFormFile jsonFile, Guid currentUserId);
    }
}
