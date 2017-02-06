using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Linq.Expressions;
using System.Data.Entity;

using Murtain.Dependency;
using Murtain.Domain.Entities;
using Murtain.Domain.UnitOfWork.ConventionalRegistras;
using Autofac.Extras.DynamicProxy2;

namespace Murtain.Domain.Repositories
{
    /// <summary>
    /// This interface must be implemented by all repositories to identify them by convention.
    /// Implement generic version instead of this one.
    /// </summary>
    public interface IRepository
    {

    }
    public interface IRepository<TEntity> : IRepository<TEntity, long> 
        where TEntity : class, IEntity<long>
    {

    }
    public interface IRepository<TEntity, TPrimaryKey> : IRepository 
        where TEntity : class, IEntity<TPrimaryKey>
    {
        IQueryable<TEntity> Sources { get; }
        IQueryable<TEntity> Models { get; }

        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes = null);
        IQueryable<TEntity> Get(IQuery<TEntity> query, Expression<Func<TEntity, object>> includes = null);

        TEntity Add(TEntity model);
        Task<TEntity> InsertAsync(TEntity model);
        IEnumerable<TEntity> AddRange(IEnumerable<TEntity> models);

        TEntity Update(TEntity model);
        TEntity UpdateProperty(TEntity model, Expression<Func<TEntity, object>> lambda);
        TEntity UpdateCompare(TEntity model, TEntity source);

        TEntity Remove(TEntity model);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> models);
        TEntity Remove(TPrimaryKey key);
        IEnumerable<TEntity> RemoveRange(IEnumerable<TPrimaryKey> keys);

        bool Any(Expression<Func<TEntity, bool>> lambda);

        int Count();
        int Count(Expression<Func<TEntity, bool>> lambda);

        TEntity Find(TPrimaryKey key);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes);
    }
}
