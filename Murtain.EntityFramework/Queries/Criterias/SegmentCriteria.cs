using System;
using System.Linq.Expressions;

using Murtain.EntityFramework.Extentions;

namespace Murtain.EntityFramework.Queries.Criterias
{
    public abstract class SegmentCriteria<TEntity, TProperty, TValue> : CriteriaBase<TEntity>
        where TEntity : class
        where TValue : struct
    {
        protected SegmentCriteria(Expression<Func<TEntity, TProperty>> propertyExpression, TValue? min, TValue? max)
        {
            Builder = new ExpressionsBuilder<TEntity>();
            PropertyExpression = propertyExpression;
            Min = min;
            Max = max;
            if (IsMinGreaterMax(min, max))
            {
                Min = max;
                Max = min;
            }
        }
        protected abstract bool IsMinGreaterMax(TValue? min, TValue? max);
        public Expression<Func<TEntity, TProperty>> PropertyExpression { get; set; }
        private ExpressionsBuilder<TEntity> Builder { get; set; }
        public TValue? Min { get; set; }
        public TValue? Max { get; set; }
        public override Expression<Func<TEntity, bool>> GetPredicate()
        {
            var first = CreateLeftExpression();
            var second = CreateRightExpression();
            return Builder.ToLambda(first.And(second));
        }
        private Expression CreateLeftExpression()
        {
            if (Min == null)
                return null;
            return Builder.Create(PropertyExpression, Operator.GreaterEqual, GetMinValue());
        }
        private Expression CreateRightExpression()
        {
            if (Max == null)
                return null;
            return Builder.Create(PropertyExpression, GetMaxOperator(), GetMaxValue());
        }
        protected virtual TValue? GetMinValue()
        {
            return Min;
        }
        protected virtual TValue? GetMaxValue()
        {
            return Max;
        }
        protected virtual Operator GetMaxOperator()
        {
            return Operator.LessEqual;
        }
    }
}
