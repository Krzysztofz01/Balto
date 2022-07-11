using Balto.Domain.Tags;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.MySql.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly BaltoDbContext _context;

        public TagRepository(BaltoDbContext dbContext) =>
            _context = dbContext ?? throw new ArgumentNullException(nameof(dbContext));


        public Task Add(Tag tag)
        {
            _ = _context.Tags.Add(tag);
            return Task.CompletedTask;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Tags
                .AsNoTracking()
                .AnyAsync(i => i.Id == id);
        }

        public async Task<Tag> Get(Guid id)
        {
            return await _context.Tags
                .FirstAsync(i => i.Id == id);
        }
    }
}
