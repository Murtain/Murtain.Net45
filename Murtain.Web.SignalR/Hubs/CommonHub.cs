using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;
using Murtain.Dependency;
using Murtain.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Murtain.Extensions;
using Microsoft.AspNet.SignalR.Hubs;
using Murtain.Threading;

namespace Murtain.Web.SignalR.Hubs
{
    [HubName("commonHub")]
    public class CommonHub : Hub
    {
        private readonly IOnlineClientManager onlineClientManager;
        public ILogger Logger { get; set; }

        public CommonHub(IOnlineClientManager onlineClientManager)
        {
            this.onlineClientManager = onlineClientManager;
            Logger = NullLogger.Instance;
        }

        public async override Task OnConnected()
        {
            await base.OnConnected();

            var client = new OnlineClient()
            {
                ConnectionId = Context.ConnectionId,
                IpAddress = GetIpAddressOfClient(),
                UserId = Context.QueryString["UserId"],
                ConnectTime = DateTime.Now
            };

            Logger.Debug("A client is connected: " + client);

            this.onlineClientManager.Add(client);
        }

        public async override Task OnDisconnected(bool stopCalled)
        {
            await base.OnDisconnected(stopCalled);

            Logger.Debug("A client is disconnected: " + Context.ConnectionId);

            try
            {
                this.onlineClientManager.Remove(Context.ConnectionId);
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public IReadOnlyList<IOnlineClient> GetOnlineClients()
        {
            return this.onlineClientManager.GetAllClients();
        }

        private string GetIpAddressOfClient()
        {
            try
            {
                return Context.Request.Environment["server.RemoteIpAddress"].ToString();
            }
            catch (Exception ex)
            {
                Logger.Error("Can not find IP address of the client! connectionId: " + Context.ConnectionId);
                Logger.Error(ex.Message, ex);
                return "";
            }
        }

    }
}
