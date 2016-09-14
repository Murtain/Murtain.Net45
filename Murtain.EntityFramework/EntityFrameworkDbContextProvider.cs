using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.UnitOfWork;
using Murtain.EntityFramework.Extensions;

namespace Murtain.EntityFramework
{
    /// <summary>
    /// Implements <see cref="IDbContextProvider{TDbContext}"/> that gets DbContext from
    /// active unit of work.
    /// </summary>
    /// <typeparam name="TDbContext">Type of the DbContext</typeparam>
    public class EntityFrameworkDbContextProvider<TDbContext> : IEntityFrameworkDbContextProvider<TDbContext>
        where TDbContext : DbContext
    {

        private readonly IUnitOfWorkManager unitOfWorkManager;

        /// <summary>
        /// Creates a new <see cref="EntityFrameworkDbContextProvider{TDbContext}"/>.
        /// </summary>
        /// <param name="currentUnitOfWorkProvider"></param>
        public EntityFrameworkDbContextProvider(IUnitOfWorkManager unitOfWorkManager)
        {
            this.unitOfWorkManager = unitOfWorkManager;
        }


        /// <summary>
        /// Gets the DbContext.
        /// </summary>
        public TDbContext GetDbContext()
        {
            return unitOfWorkManager.Current.GetDbContext<TDbContext>();
        }
    }
}
