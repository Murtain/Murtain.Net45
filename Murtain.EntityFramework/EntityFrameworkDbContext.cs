using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Data.Entity.Validation;
using System.Threading;
using System.Threading.Tasks;

using Murtain.Runtime.Session;
using Murtain.Domain.Entities;
using Murtain.Logging;
using Murtain.Domain.Entities.Audited;

namespace Murtain.EntityFramework
{
    /// <summary>
    /// Base class for all DbContext classes in the application.
    /// </summary>   
    public abstract class EntityFrameworkDbContext : DbContext
    {
        public IAppSession AppSession { get; set; }
        protected EntityFrameworkDbContext()
        {
            AppSession = NullAppSession.Instance;
        }
        protected EntityFrameworkDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            AppSession = NullAppSession.Instance;
        }
        public virtual void Initialize()
        {
            Database.Initialize(false);
        }
        public override int SaveChanges()
        {
            try
            {
                ApplyAbpConcepts();
                return base.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                ApplyAbpConcepts();
                return await base.SaveChangesAsync(cancellationToken);
            }
            catch (DbEntityValidationException ex)
            {
                LogDbEntityValidationException(ex);
                throw;
            }
        }
        protected virtual void ApplyAbpConcepts()
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (entry.Entity is ICreateAudited)
                        {
                            entry.Cast<ICreateAudited>().Entity.CreateUser = AppSession.UserId;
                            entry.Cast<ICreateAudited>().Entity.CreateTime = DateTime.Now;
                        }
                        if (entry.Entity is IAudited)
                        {
                            entry.Cast<IAudited>().Entity.CreateUser = AppSession.UserId;
                            entry.Cast<IAudited>().Entity.CreateTime = DateTime.Now;
                        }
                        if (entry.Entity is ISoftDelete)
                        {
                            entry.Cast<ISoftDelete>().Entity.IsDeleted = false;
                        }
                        break;
                    case EntityState.Modified:
                        if (entry.Entity is ICreateAudited)
                        {
                            entry.Cast<ICreateAudited>().Property(x => x.CreateTime).IsModified = false;
                            entry.Cast<ICreateAudited>().Property(x => x.CreateUser).IsModified = false;
                        }
                        if (entry.Entity is IChangeAudited)
                        {
                            entry.Cast<IChangeAudited>().Entity.ChangeUser = AppSession.UserId;
                            entry.Cast<IChangeAudited>().Entity.ChangeTime = DateTime.Now;
                        }
                        if (entry.Entity is IAudited)
                        {
                            entry.Cast<IAudited>().Property(x => x.CreateTime).IsModified = false;
                            entry.Cast<IAudited>().Property(x => x.CreateUser).IsModified = false;

                            entry.Cast<IAudited>().Entity.ChangeUser = AppSession.UserId;
                            entry.Cast<IAudited>().Entity.ChangeTime = DateTime.Now;
                        }

                        break;
                    case EntityState.Deleted:
                        if (entry.Entity is ISoftDelete)
                        {
                            entry.Cast<ISoftDelete>().State = EntityState.Unchanged;
                            entry.Cast<ISoftDelete>().Entity.IsDeleted = true;
                        }
                        break;
                }
            }
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        private void LogDbEntityValidationException(DbEntityValidationException exception)
        {
            LogHelper.Logger.Error("There are some validation errors while saving changes in EntityFramework:");
            foreach (var ve in exception.EntityValidationErrors.SelectMany(eve => eve.ValidationErrors))
            {
                LogHelper.Logger.Error(" - " + ve.PropertyName + ": " + ve.ErrorMessage);
            }
        }
    }
}
