using System;
using System.Linq.Expressions;

namespace Murtain.EntityFramework.Queries.Criterias
{
    internal class Criteria<TEntity> : CriteriaBase<TEntity> where TEntity : class
    {
        public Criteria(Expression<Func<TEntity, bool>> predicate)
        {
            Predicate = predicate;
        }
    }
}
