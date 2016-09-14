using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murtain.Domain.UnitOfWork;
using System.Data.Entity;

namespace Murtain.EntityFramework.Extensions
{
    /// <summary>
    /// Extension methods for UnitOfWork.
    /// </summary>
    public static class EntityFrameworkUnitOfWorkExtensions
    {
        /// <summary>
        /// Gets a DbContext as a part of active unit of work.
        /// This method can be called when current unit of work is an <see cref="EntityFrameworkUnitOfWork"/>.
        /// </summary>
        /// <typeparam name="TDbContext">Type of the DbContext</typeparam>
        /// <param name="unitOfWork">Current (active) unit of work</param>
        public static TDbContext GetDbContext<TDbContext>(this IActiveUnitOfWork unitOfWork)
            where TDbContext : DbContext
        {
            if (unitOfWork == null)
            {
                throw new ArgumentNullException("unitOfWork");
            }

            if (!(unitOfWork is EntityFrameworkUnitOfWork))
            {
                throw new ArgumentException("[Framework]unitOfWork is not type of " + typeof(EntityFrameworkUnitOfWork).FullName, "unitOfWork");
            }

            return (unitOfWork as EntityFrameworkUnitOfWork).GetOrCreateDbContext<TDbContext>();
        }
    }
}
