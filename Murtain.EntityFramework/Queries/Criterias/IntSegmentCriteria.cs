using System;
using System.Linq.Expressions;

using Murtain.EntityFramework.Extentions;

namespace Murtain.EntityFramework.Queries.Criterias
{
    public class IntSegmentCriteria<TEntity, TProperty> : SegmentCriteria<TEntity, TProperty, int> where TEntity : class
    {
        public IntSegmentCriteria(Expression<Func<TEntity, TProperty>> propertyExpression, int? min, int? max)
            : base(propertyExpression, min, max)
        {
        }
        protected override bool IsMinGreaterMax(int? min, int? max)
        {
            return min > max;
        }
    }
}
