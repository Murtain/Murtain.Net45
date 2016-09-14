using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.EntityFramework.Queries.Criterias
{
    public interface ICriteria<T> where T : class
    {
        Expression<Func<T, bool>> GetPredicate();
    }
    public interface ICriteria
    {
        string GetPredicate();
        object[] GetValues();
    }
}
