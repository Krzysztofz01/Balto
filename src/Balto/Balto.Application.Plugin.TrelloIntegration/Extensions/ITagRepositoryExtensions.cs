using Balto.Domain.Tags;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Balto.Application.Plugin.TrelloIntegration.Extensions
{
    internal static class ITagRepositoryExtensions
    {
        public async static Task Add(this ITagRepository tagRepository, IEnumerable<Tag> tags)
        {
            foreach (var tag in tags) await tagRepository.Add(tag);
        }
    }
}
