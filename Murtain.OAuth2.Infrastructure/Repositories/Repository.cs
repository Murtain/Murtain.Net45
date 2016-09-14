using Murtain.Domain.Entities;
using Murtain.EntityFramework;

namespace Murtain.OAuth2.Infrastructure.Repositories
{
    public class Repository<TEntity> : Repository<ModelsContainer, TEntity, long>
        where TEntity : class, IEntity<long>
    {
        public Repository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }

    public class Repository<TEntity, TPrimaryKey> : Repository<ModelsContainer, TEntity, TPrimaryKey>
    where TEntity : class, IEntity<TPrimaryKey>
    {
        public Repository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {
        }
    }
}