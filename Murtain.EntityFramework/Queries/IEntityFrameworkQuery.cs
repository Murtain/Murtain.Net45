using Murtain.Domain.Entities;
using Murtain.EntityFramework.Queries.Criterias;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.EntityFramework.Queries
{
    public interface IEntityFrameworkQuery<TEntity> : IEntityFrameworkQuery<TEntity, long> where TEntity : class
    {

    }

    public interface IEntityFrameworkQuery<TEntity, TPrimaryKey> : IQuery<TEntity> where TEntity : class
    {
        IEntityFrameworkQuery<TEntity, TPrimaryKey> Filter(Expression<Func<TEntity, bool>> predicate, bool isInvalid = false);
        IEntityFrameworkQuery<TEntity, TPrimaryKey> Filter(string propertyName, object value, Operator opt = Operator.Equal);
        IEntityFrameworkQuery<TEntity, TPrimaryKey> Filter(ICriteria<TEntity> criteria);
        IEntityFrameworkQuery<TEntity, TPrimaryKey> FilterInt<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, int? min, int? max);
        IEntityFrameworkQuery<TEntity, TPrimaryKey> FilterDouble<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, double? min, double? max);
        IEntityFrameworkQuery<TEntity, TPrimaryKey> FilterDate<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max);
        IEntityFrameworkQuery<TEntity, TPrimaryKey> FilterDateTime<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, DateTime? min, DateTime? max);
        IEntityFrameworkQuery<TEntity, TPrimaryKey> FilterDecimal<TProperty>(Expression<Func<TEntity, TProperty>> propertyExpression, decimal? min, decimal? max);
        IEntityFrameworkQuery<TEntity, TPrimaryKey> And(IEntityFrameworkQuery<TEntity, TPrimaryKey> query);
        IEntityFrameworkQuery<TEntity, TPrimaryKey> And(Expression<Func<TEntity, bool>> predicate);
        IEntityFrameworkQuery<TEntity, TPrimaryKey> Or(IEntityFrameworkQuery<TEntity, TPrimaryKey> query);
        IEntityFrameworkQuery<TEntity, TPrimaryKey> Or(Expression<Func<TEntity, bool>> predicate);
    }
}
