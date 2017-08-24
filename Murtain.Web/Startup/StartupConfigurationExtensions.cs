using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using Murtain.Caching;
using Murtain.Collections;
using Murtain.Configuration.Startup;
using Murtain.Dependency;
using Murtain.Dependency.ConventionalRegistrars;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;

namespace Murtain.Configuration.Startup
{
    public static class StartupConfigurationExtensions
    {
        private const string assemblyLoaderPartner = "";

        public static StartupConfiguration RegisterWebMvcApplication(this StartupConfiguration bootstrap, string assemblyLoaderPartner = assemblyLoaderPartner, params IModule[] modules)
        {
            var assemblies = new AssemblyLoader(assemblyLoaderPartner).FilterSystemAssembly(BuildManager.GetReferencedAssemblies().Cast<Assembly>());

            IocManager.Instance.AddConventionalRegistrar(new ControllerConventionalRegistrar());
            IocManager.Instance.RegisterAssemblyByConvention(assemblies, modules);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(IocManager.Instance.Container));

            return bootstrap;
        }
        public static StartupConfiguration RegisterWebApiApplication(this StartupConfiguration bootstrap, string assemblyLoaderPartner = assemblyLoaderPartner, params Autofac.Module[] modules)
        {
            var assemblies = new AssemblyLoader(assemblyLoaderPartner).FilterSystemAssembly(BuildManager.GetReferencedAssemblies().Cast<Assembly>());

            IocManager.Instance.AddConventionalRegistrar(new ControllerConventionalRegistrar());
            IocManager.Instance.AddConventionalRegistrar(new ApiControllerConventionalRegistrar());

            IocManager.Instance.RegisterAssemblyByConvention(assemblies, modules);

            DependencyResolver.SetResolver(new AutofacDependencyResolver(IocManager.Instance.Container));

            HttpConfiguration configuration = GlobalConfiguration.Configuration;
            configuration.DependencyResolver = new AutofacWebApiDependencyResolver(IocManager.Instance.Container);

            return bootstrap;
        }

    }
}
