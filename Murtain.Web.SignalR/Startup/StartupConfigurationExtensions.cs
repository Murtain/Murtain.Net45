﻿using Microsoft.AspNet.SignalR;
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
using Murtain.Web.SignalR;

namespace Murtain.Configuration.Startup
{
    public static class StartupConfigurationExtensions
    {
        public static StartupConfiguration UseSignalR(this StartupConfiguration bootstrap)
        {
            IocManager.Container.RegisterIfNot<IOnlineClientManager, OnlineClientManager>();
            IocManager.Container.RegisterIfNot<IRealTimeNotifier, SignalRRealTimeNotifier>(DependencyLifeStyle.Transient);

            var builder = new ContainerBuilder();

            builder.RegisterType<CommonHub>()
                .As<Hub>()
                .AsImplementedInterfaces()
                .AsSelf();

            builder.Update(IocManager.Container.IocContainer);

            GlobalHost.DependencyResolver = new AutofacDependencyResolver(IocManager.Container.IocContainer);
            return bootstrap;
        }
    }
}
