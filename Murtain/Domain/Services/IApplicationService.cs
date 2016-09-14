using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Dependency;
using Autofac.Extras.DynamicProxy2;
using Murtain.Domain.UnitOfWork.ConventionalRegistras;

namespace Murtain.Domain.Services
{
    /// <summary>
    /// This interface must be implemented by all application services to identify them by convention.
    /// </summary>
    public interface IApplicationService
    {

    }
}
