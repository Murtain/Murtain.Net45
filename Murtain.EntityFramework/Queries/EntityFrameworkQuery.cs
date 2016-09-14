using Murtain.EntityFramework.Extentions;
using Murtain.EntityFramework.Queries.Criterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.EntityFramework.Queries
{
    public class EntityFrameworkQuery<TEntity> : EntityFrameworkQuery<TEntity, long>
      where TEntity : class
    {

    }
    public class EntityFrameworkQuery<TEntity, TPrimaryKey> : IEntityFrameworkQuery<TEntity, TPrimaryKey>
    where TEntity : class
    {

        private ICriteria<TEntity> criteria;
        private OrderByBuilder orderBuilder;

        public EntityFrameworkQuery()
        {
            orderBuilder = new OrderByBuilder();
        }
        public Expression<Func<TEntity, bool>> GetPredicate()
        {
            if (criteria == null)
            {
                return x => true;
            }
            return criteria.GetPredicate();
        }

        public IEntityFrameworkQuery<TEntity, TPrimaryKey> Filter(Expression<Func<TEntity, bool>> predicate,bool isInvalid = false)
        {
            if (predicate == null || predicate.Value() == null || isInvalid)
            {
                return this;
            }
            And(predicate);

            return this;
        }
        public IEntityFrameworkQuery<TEntity, TPrimaryKey> Filter(string propertyName, object value, Operator opt = Operator.Equal)
        {
            return Filter(LambdaBuilder.ParsePredicate<TEntity>(propertyName, value, opt));
        }
        public IEntityFrameworkQuery<TEntity, TPrimaryKey> Filter(ICriteria<TEntity> criteria)
        {
            And(criteria.GetPredicate());
            return this;
        }
        public IEntityFrameworkQuery<TEntity, TPrimaryKey> FilterInt<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, int? min, int? max)
        {
            return Filter(new IntSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max));
        }
        public IEntityFrameworkQuery<TEntity, TPrimaryKey> FilterDouble<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, double? min, double? max)
        {
            return Filter(new DoubleSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max));
        }
        public IEntityFrameworkQuery<TEntity, TPrimaryKey> FilterDate<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max)
        {
            return Filter(new DateSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max));
        }
        public IEntityFrameworkQuery<TEntity, TPrimaryKey> FilterDateTime<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max)
        {
            return Filter(new DateTimeSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max));
        }
        public IEntityFrameworkQuery<TEntity, TPrimaryKey> FilterDecimal<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, decimal? min, decimal? max)
        {
            return Filter(new DecimalSegmentCriteria<TEntity, TProperty>(propertyExpression, min, max));
        }
        public IEntityFrameworkQuery<TEntity, TPrimaryKey> And(IEntityFrameworkQuery<TEntity, TPrimaryKey> query)
        {
            return And(query.GetPredicate());
        }
        public IEntityFrameworkQuery<TEntity, TPrimaryKey> And(Expression<Func<TEntity, bool>> predicate)
        {
            if (criteria == null)
            {
                criteria = new Criteria<TEntity>(predicate);
                return this;
            }
            criteria = new AndCriteria<TEntity>(criteria.GetPredicate(), predicate);
            return this;
        }
        public IEntityFrameworkQuery<TEntity, TPrimaryKey> Or(IEntityFrameworkQuery<TEntity, TPrimaryKey> query)
        {
            return Or(query.GetPredicate());
        }
        public IEntityFrameworkQuery<TEntity, TPrimaryKey> Or(Expression<Func<TEntity, bool>> predicate)
        {
            if (criteria == null)
            {
                criteria = new Criteria<TEntity>(predicate);
                return this;
            }
            criteria = new OrCriteria<TEntity>(criteria.GetPredicate(), predicate);
            return this;
        }

    }
}
