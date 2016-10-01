using Microsoft.AspNet.SignalR;
using Murtain.Configuration.Startup;
using Murtain.Dependency;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR.Hubs;
using Murtain.Web.SignalR.Hubs;
using Murtain.Web.SignalR.Notifications;
using Autofac;
using Autofac.Integration.SignalR;

namespace Murtain.Configuration.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseSignalR(this StartupConfiguration bootstrap)
        {
            IocManager.Instance.RegisterIfNot<IOnlineClientManager, OnlineClientManager>();
            IocManager.Instance.RegisterIfNot<IRealTimeNotifier, SignalRRealTimeNotifier>(DependencyLifeStyle.Transient);

            var builder = new ContainerBuilder();

            builder.RegisterType<CommonHub>()
                .As<Hub>()
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Update(IocManager.Instance.IocContainer);

            GlobalHost.DependencyResolver = new AutofacDependencyResolver(IocManager.Instance.IocContainer);
            return bootstrap;
        }
    }
}
