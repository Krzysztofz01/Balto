using Balto.Domain.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Balto.Domain.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<TEntity> SkipDeleted<TEntity>(this IEnumerable<TEntity> collection) where TEntity : Entity
        {
            return collection.Where(e => e.DeletedAt == null);
        }
    }
}
