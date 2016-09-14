using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Entities
{
    public interface IQuery<TEntity> where TEntity : class
    {
        Expression<Func<TEntity, bool>> GetPredicate();
    }
}
