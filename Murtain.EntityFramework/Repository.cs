using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Entities;
using Murtain.Domain.UnitOfWork;
using Murtain.Domain.Repositories;
using System.Data;
using System.Data.Entity;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Linq.Expressions;

namespace Murtain.EntityFramework
{
    public class Repository<TDbContext, TEntity> : Repository<TDbContext, TEntity, long>
        where TEntity : class, IEntity<long>
        where TDbContext : DbContext
    {
        protected Repository(IEntityFrameworkDbContextProvider<TDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
    public class Repository<TDbContext, TEntity, TPrimaryKey> : IRepository<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : DbContext
    {
        private TDbContext dbContext
        {
            get
            {
                return dbContextProvider.GetDbContext();
            }
        }

        private readonly IEntityFrameworkDbContextProvider<TDbContext> dbContextProvider;
        protected Repository(IEntityFrameworkDbContextProvider<TDbContext> dbContextProvider)
        {
            this.dbContextProvider = dbContextProvider;
        }

        public virtual IDbConnection Connection
        {
            get
            {
                return dbContext.Database.Connection;
            }
        }

        public virtual IQueryable<TEntity> Sources
        {
            get
            {
                return dbContext.Set<TEntity>().AsQueryable();
            }
        }
        public virtual IQueryable<TEntity> Models
        {
            get
            {
                return dbContext.Set<TEntity>().AsNoTracking();
            }
        }

        public virtual IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> temp = dbContext.Set<TEntity>().Where(lambda);
            if (includes != null)
            {
                foreach (MemberInfo me in ((dynamic)includes.Body).Members)
                {
                    temp = temp.Include(me.Name);
                }
            }
            return temp.AsNoTracking();
        }
        public virtual IQueryable<TEntity> Get(IQuery<TEntity> query, Expression<Func<TEntity, object>> includes = null)
        {
            IQueryable<TEntity> temp = dbContext.Set<TEntity>();
            if (includes != null)
            {
                foreach (MemberInfo me in ((dynamic)includes.Body).Members)
                {
                    temp = temp.Include(me.Name);
                }
            }
            var predicate = query.GetPredicate();
            if (predicate == null)
            {
                return temp;
            }
            return temp.AsNoTracking().Where(predicate);
        }

        private void SaveChanges()
        {
            dbContext.SaveChanges();
        }
        private void SaveChangesAsync()
        {
            dbContext.SaveChangesAsync();
        }

        public virtual TEntity Add(TEntity model)
        {
            return dbContext.Set<TEntity>().Add(model);
        }
        public virtual IEnumerable<TEntity> AddRange(IEnumerable<TEntity> models)
        {
            return dbContext.Set<TEntity>().AddRange(models);
        }

        public virtual void Update(TEntity model)
        {
            AttachIfNot(model);
            dbContext.Entry(model).State = EntityState.Modified;
        }
        public virtual void UpdateProperty(TEntity model, Expression<Func<TEntity, object>> lambda)
        {
            ReadOnlyCollection<MemberInfo> memberInfos = ((dynamic)lambda.Body).Members;
            AttachIfNot(model);
            foreach (MemberInfo memberInfo in memberInfos)
            {
                dbContext.Entry(model).Property(memberInfo.Name).IsModified = true;
            }
        }
        public virtual void UpdateCompare(TEntity model, TEntity source)
        {
            dbContext.Entry(source).CurrentValues.SetValues(model);
        }

        public virtual TEntity Remove(TEntity model)
        {
            return dbContext.Set<TEntity>().Remove(model);
        }
        public virtual IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> models)
        {
            return dbContext.Set<TEntity>().RemoveRange(models);
        }
        public virtual TEntity Remove(TPrimaryKey key)
        {
            return Remove(dbContext.Set<TEntity>().Find(key));
        }
        public virtual IEnumerable<TEntity> RemoveRange(IEnumerable<TPrimaryKey> keys)
        {
            List<TEntity> range = new List<TEntity>();
            foreach (var key in keys)
            {
                var model = dbContext.Set<TEntity>().Find(key);
                range.Add(model);
            }
            return RemoveRange(range);
        }

        public virtual bool Any(System.Linq.Expressions.Expression<Func<TEntity, bool>> lambda)
        {
            return dbContext.Set<TEntity>().Any(lambda);
        }

        public virtual int Count()
        {
            return dbContext.Set<TEntity>().Count();
        }
        public virtual int Count(System.Linq.Expressions.Expression<Func<TEntity, bool>> lambda)
        {
            return dbContext.Set<TEntity>().Count(lambda);
        }

        public virtual TEntity Find(TPrimaryKey key)
        {
            return dbContext.Set<TEntity>().Find(key);
        }
        public virtual TEntity FirstOrDefault(System.Linq.Expressions.Expression<Func<TEntity, bool>> lambda)
        {
            return dbContext.Set<TEntity>().FirstOrDefault(lambda);
        }
        public virtual TEntity FirstOrDefault(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes)
        {
            IQueryable<TEntity> temp = dbContext.Set<TEntity>();
            if (includes != null)
            {
                foreach (MemberInfo me in ((dynamic)includes.Body).Members)
                {
                    temp = temp.Include(me.Name);
                }
            }
            return temp.FirstOrDefault(lambda);
        }

        protected virtual void AttachIfNot(TEntity model)
        {
            if (!dbContext.Set<TEntity>().Local.Contains(model))
            {
                dbContext.Set<TEntity>().Attach(model);
            }
        }

        public virtual Task<IQueryable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes = null)
        {
            return Task.FromResult(Get(lambda, includes));
        }
        public virtual Task<IQueryable<TEntity>> GetAsync(IQuery<TEntity> query, Expression<Func<TEntity, object>> includes = null)
        {
            return Task.FromResult(Get(query, includes));
        }
        public virtual Task<TEntity> AddAsync(TEntity model)
        {
            return Task.FromResult(Add(model));
        }
        public virtual Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> models)
        {
            return Task.FromResult(AddRange(models));
        }

        public virtual Task UpdateAsync(TEntity model)
        {
            Update(model);
            return Task.FromResult(0);
        }
        public virtual Task UpdatePropertyAsync(TEntity model, Expression<Func<TEntity, object>> lambda)
        {
            UpdateProperty(model, lambda);
            return Task.FromResult(0);
        }
        public virtual Task UpdateCompareAsync(TEntity model, TEntity source)
        {
            UpdateCompare(model, source);
            return Task.FromResult(0);
        }

        public virtual Task<TEntity> RemoveAsync(TEntity model)
        {
            return Task.FromResult(Remove(model));
        }
        public virtual Task<IEnumerable<TEntity>> RemoveRangeAsync(IEnumerable<TEntity> models)
        {
            return Task.FromResult(RemoveRange(models));
        }
        public virtual Task<TEntity> RemoveAsync(TPrimaryKey key)
        {
            return Task.FromResult(Remove(key));
        }
        public virtual Task<IEnumerable<TEntity>> RemoveRangeAsync(IEnumerable<TPrimaryKey> keys)
        {
            return Task.FromResult(RemoveRange(keys));
        }

        public virtual Task<bool> AnyAsync(Expression<Func<TEntity, bool>> lambda)
        {
            return Task.FromResult(Any(lambda));
        }

        public virtual Task<int> CountAsync()
        {
            return Task.FromResult(Count());
        }
        public virtual Task<int> CountAsync(Expression<Func<TEntity, bool>> lambda)
        {
            return Task.FromResult(Count(lambda));
        }

        public virtual Task<TEntity> FindAsync(TPrimaryKey key)
        {
            return Task.FromResult(Find(key));
        }
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> lambda)
        {
            return Task.FromResult(FirstOrDefault(lambda));
        }
        public virtual Task<TEntity> FirstOrDefaultAsync(Expression<Func<TEntity, bool>> lambda, Expression<Func<TEntity, object>> includes)
        {
            return Task.FromResult(FirstOrDefault(lambda, includes));
        }
    }

}
