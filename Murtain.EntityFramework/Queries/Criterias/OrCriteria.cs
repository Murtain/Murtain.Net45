using System;
using System.Linq.Expressions;

using Murtain.EntityFramework.Extentions;

namespace Murtain.EntityFramework.Queries.Criterias
{

    public class OrCriteria<TEntity> : CriteriaBase<TEntity> where TEntity : class
    {
        public OrCriteria(Expression<Func<TEntity, bool>> first, Expression<Func<TEntity, bool>> second)
        {
            Predicate = first.Or(second);
        }
    }
}
