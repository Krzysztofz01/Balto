using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Balto.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<TEntity> Get(long id);
        Task<TEntity> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity entity);
        Task AddRange(TEntity entity);
        Task Remove(TEntity entity);
        Task RemoveRange(TEntity entity);
    }
}
