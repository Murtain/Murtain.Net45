using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

using Murtain.Dependency;
using Murtain.Extensions;
using Murtain.Domain.UnitOfWork;
using Murtain.Domain.UnitOfWork.Configuration;
using Castle.Core.Internal;
using EntityFramework.DynamicFilters;

namespace Murtain.EntityFramework
{
    /// <summary>
    /// Implements Unit of work for Entity Framework.
    /// </summary>
    public class EntityFrameworkUnitOfWork : UnitOfWorkBase
    {
        protected readonly IDictionary<Type, DbContext> ActiveDbContexts;

        protected TransactionScope CurrentTransaction;

        /// <summary>
        /// Creates a new <see cref="EntityFrameworkUnitOfWork"/>.
        /// </summary>
        public EntityFrameworkUnitOfWork(IUnitOfWorkDefaultOptionsConfiguration defaultOptions)
            : base(defaultOptions)
        {
            ActiveDbContexts = new Dictionary<Type, DbContext>();
        }

        protected override void BeginUow()
        {
            if (Options.IsTransactional == true)
            {
                var transactionOptions = new TransactionOptions
                {
                    IsolationLevel = Options.IsolationLevel.GetValueOrDefault(IsolationLevel.ReadUncommitted),
                };

                if (Options.Timeout.HasValue)
                {
                    transactionOptions.Timeout = Options.Timeout.Value;
                }

                CurrentTransaction = new TransactionScope(
                    Options.Scope.GetValueOrDefault(TransactionScopeOption.Required),
                    transactionOptions,
                    Options.AsyncFlowOption.GetValueOrDefault(TransactionScopeAsyncFlowOption.Enabled)
                    );
            }
        }

        public override void SaveChanges()
        {
            ActiveDbContexts.Values.ForEach(SaveChangesInDbContext);
        }

        public override async Task SaveChangesAsync()
        {
            foreach (var dbContext in ActiveDbContexts.Values)
            {
                await SaveChangesInDbContextAsync(dbContext);
            }
        }

        protected override void CompleteUow()
        {
            SaveChanges();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }
        }

        protected override async Task CompleteUowAsync()
        {
            await SaveChangesAsync();
            if (CurrentTransaction != null)
            {
                CurrentTransaction.Complete();
            }
        }

        protected override void ApplyDisableFilter(string filterName)
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                activeDbContext.DisableFilter(filterName);
            }
        }

        protected override void ApplyEnableFilter(string filterName)
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                activeDbContext.EnableFilter(filterName);
            }
        }

        protected override void ApplyFilterParameterValue(string filterName, string parameterName, object value)
        {
            foreach (var activeDbContext in ActiveDbContexts.Values)
            {
                if (value != null && value.GetType() == typeof(Func<object>))
                {
                    activeDbContext.SetFilterScopedParameterValue(filterName, parameterName, (Func<object>)value);
                }
                else
                {
                    activeDbContext.SetFilterScopedParameterValue(filterName, parameterName, value);
                }
            }
        }

        public virtual TDbContext GetOrCreateDbContext<TDbContext>()
            where TDbContext : DbContext
        {
            DbContext dbContext;
            if (!ActiveDbContexts.TryGetValue(typeof(TDbContext), out dbContext))
            {
                dbContext = (TDbContext)IocManager.Instance.Resolve(typeof(TDbContext));

                foreach (var filter in Filters)
                {
                    if (filter.IsEnabled)
                    {
                        dbContext.EnableFilter(filter.FilterName);
                    }
                    else
                    {
                        dbContext.DisableFilter(filter.FilterName);
                    }

                    foreach (var filterParameter in filter.FilterParameters)
                    {
                        if (filterParameter.Value != null && filterParameter.Value.GetType() == typeof(Func<object>))
                        {
                            dbContext.SetFilterScopedParameterValue(filter.FilterName, filterParameter.Key, (Func<object>)filterParameter.Value);
                        }
                        else
                        {
                            dbContext.SetFilterScopedParameterValue(filter.FilterName, filterParameter.Key, filterParameter.Value);
                        }
                    }
                }

                ActiveDbContexts[typeof(TDbContext)] = dbContext;
            }

            return (TDbContext)dbContext;
        }

        protected override void DisposeUow()
        {
            ActiveDbContexts.Values.ForEach(Release);

            if (CurrentTransaction != null)
            {
                CurrentTransaction.Dispose();
            }
        }

        protected virtual void SaveChangesInDbContext(DbContext dbContext)
        {
            dbContext.SaveChanges();
        }

        protected virtual async Task SaveChangesInDbContextAsync(DbContext dbContext)
        {
             await Task.FromResult(dbContext.SaveChanges());
        }

        protected virtual void Release(DbContext dbContext)
        {
            dbContext.Dispose();
        }
    }
}
