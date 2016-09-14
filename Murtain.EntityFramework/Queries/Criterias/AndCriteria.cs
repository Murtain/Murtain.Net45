using System;
using System.Linq.Expressions;

using Murtain.EntityFramework.Extentions;

namespace Murtain.EntityFramework.Queries.Criterias
{

    public class AndCriteria<TEntity> : CriteriaBase<TEntity> where TEntity : class
    {
        public AndCriteria(Expression<Func<TEntity, bool>> first, Expression<Func<TEntity, bool>> second)
        {
            Predicate = first.And(second);
        }
    }
}
