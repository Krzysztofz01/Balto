using Balto.Domain.Core.Model;
using System.Collections.Generic;
using System.Linq;

namespace Balto.Domain.Core.Extensions
{
    public static class ListExtensions
    {
        public static List<TEntity> SkipDeleted<TEntity>(this List<TEntity> collection) where TEntity : Entity
        {
            return collection.Where(e => e.DeletedAt == null).ToList();
        }
    }
}
