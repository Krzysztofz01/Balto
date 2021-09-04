using Balto.Domain.Aggregates.Objective;
using Balto.Infrastructure.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Balto.Infrastructure.SqlServer.Repositories
{
    public class ObjectiveRepository : IObjectiveRepository, IDisposable
    {
        private readonly BaltoDbContext _dbContext;

        public ObjectiveRepository(BaltoDbContext dbContext)
        {
            _dbContext = dbContext ??
                throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task Add(Objective entity)
        {
            await _dbContext.AddAsync(entity);
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public async Task<bool> Exists(ObjectiveId id)
        {
            return await _dbContext.Objectives.AnyAsync(e => e.Id.Value == id.Value);
        }

        public async Task<Objective> Load(ObjectiveId id)
        {
            return await _dbContext.Objectives.SingleAsync(e => e.Id.Value == id.Value);
        }
    }
}
