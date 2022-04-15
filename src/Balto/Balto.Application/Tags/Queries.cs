using Balto.Domain.Tags;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Tags
{
    public static class Queries
    {
        public static async Task<IEnumerable<Tag>> GetAllTags(this IQueryable<Tag> tags)
        {
            return await tags
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<Tag> GetTagById(this IQueryable<Tag> tags, Guid tagId)
        {
            return await tags
                .AsNoTracking()
                .SingleAsync(p => p.Id == tagId);
        }
    }
}
