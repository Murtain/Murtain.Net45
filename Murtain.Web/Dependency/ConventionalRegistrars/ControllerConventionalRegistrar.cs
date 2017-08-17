using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Murtain.Dependency.ConventionalRegistrars
{
    /// <summary>
    /// This class is used to register basic dependency implementations such as <see cref="IController"/>.
    /// </summary>
    public class ControllerConventionalRegistrar : IConventionalDependencyRegistrar
    {
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(context.Assembly);

            builder.Update(context.IocManager.Container);
        }
    }
}
