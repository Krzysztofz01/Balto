using System;
using System.Threading.Tasks;

namespace Balto.Domain.Tags
{
    public interface ITagRepository
    {
        Task Add(Tag tag);
        Task<Tag> Get(Guid id);
        Task<bool> Exists(Guid id);
    }
}
