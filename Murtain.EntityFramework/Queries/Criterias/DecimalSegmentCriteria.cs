using System;
using System.Linq.Expressions;

using Murtain.EntityFramework.Extentions;

namespace Murtain.EntityFramework.Queries.Criterias
{

    public class DecimalSegmentCriteria<TEntity, TProperty> : SegmentCriteria<TEntity, TProperty, decimal> where TEntity : class
    {
        public DecimalSegmentCriteria(Expression<Func<TEntity, TProperty>> propertyExpression, decimal? min, decimal? max)
            : base(propertyExpression, min, max)
        {
        }

        protected override bool IsMinGreaterMax(decimal? min, decimal? max)
        {
            return min > max;
        }
    }
}
