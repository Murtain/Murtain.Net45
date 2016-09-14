using System;
using System.Linq.Expressions;

namespace Murtain.EntityFramework.Queries.Criterias
{
    public abstract class CriteriaBase<TEntity> : ICriteria<TEntity> where TEntity : class
    {
        protected Expression<Func<TEntity, bool>> Predicate { get; set; }

        public virtual Expression<Func<TEntity, bool>> GetPredicate()
        {
            return Predicate;
        }
    }
}
