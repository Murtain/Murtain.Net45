using System;
using System.Linq.Expressions;

using Murtain.EntityFramework.Extentions;

namespace Murtain.EntityFramework.Queries.Criterias
{

    public class DateTimeSegmentCriteria<TEntity, TProperty> : SegmentCriteria<TEntity, TProperty, DateTime> where TEntity : class
    {
        public DateTimeSegmentCriteria(Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max)
            : base(propertyExpression, min, max)
        {
        }


        protected override bool IsMinGreaterMax(DateTime? min, DateTime? max)
        {
            return min > max;
        }
    }
}
