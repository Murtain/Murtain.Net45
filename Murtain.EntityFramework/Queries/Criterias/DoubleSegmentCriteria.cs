using System;
using System.Linq.Expressions;

using Murtain.EntityFramework.Extentions;

namespace Murtain.EntityFramework.Queries.Criterias
{
    public class DoubleSegmentCriteria<TEntity, TProperty> : SegmentCriteria<TEntity, TProperty, double> where TEntity : class
    {
        public DoubleSegmentCriteria(Expression<Func<TEntity, TProperty>> propertyExpression, double? min, double? max)
            : base(propertyExpression, min, max)
        {
        }

        protected override bool IsMinGreaterMax(double? min, double? max)
        {
            return min > max;
        }
    }
}
