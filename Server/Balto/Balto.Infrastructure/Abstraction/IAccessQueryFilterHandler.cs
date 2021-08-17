using System;
using System.Linq.Expressions;

namespace Balto.Infrastructure.Abstraction
{
    public interface IAccessQueryFilterHandler
    {
        bool IsAllowed(Guid contentOwnerGuid);
        bool IsAllowedIgnoreLeaders(Guid contentOwnerGuid);
        Expression<Func<T, bool>> Rule<T>(Expression<Func<T, bool>> ruleBuilder);
    }
}
