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


        public async Task Add(Tag tag)
        {
            _ = await _context.Tags.AddAsync(tag);
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Tags.AnyAsync(i => i.Id == id);
        }

        public async Task<Tag> Get(Guid id)
        {
            return await _context.Tags.SingleAsync(i => i.Id == id);
        }
    }
}
