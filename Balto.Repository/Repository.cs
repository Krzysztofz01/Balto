using Balto.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly DbContext context;
        protected readonly DbSet<TEntity> entities;

        public Repository(DbContext context)
        {
            this.context = context;
            entities = context.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            await entities.AddAsync(entity);
        }

        public async Task AddRange(IEnumerable<TEntity> entityCollection)
        {
            await entities.AddRangeAsync(entityCollection);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return entities.Where(predicate);
        }

        public async Task<TEntity> Get(long id)
        {
            return await entities.FindAsync(id);
        }

        public async Task<IEnumerable<TEntity>> GetAll()
        {
            return await entities.ToListAsync();
        }

        public void Remove(TEntity entity)
        {
            entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entityCollection)
        {
            entities.RemoveRange(entityCollection);
        }

        public async Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return await entities.SingleOrDefaultAsync(predicate);
        }

        public void UpdateState(TEntity entity)
        {
            context.Entry(entity).State = EntityState.Modified;
        }

        public async Task<int> Save()
        {
            return await context.SaveChangesAsync();
        }
    }
}
