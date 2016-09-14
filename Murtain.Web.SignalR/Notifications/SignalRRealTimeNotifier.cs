using Castle.Core.Logging;
using Microsoft.AspNet.SignalR;
using Murtain.Dependency;
using Murtain.Web.SignalR.Hubs;
using Murtain.Web.SignalR.Notifications.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Web.SignalR.Notifications
{
    public class SignalRRealTimeNotifier : IRealTimeNotifier
    {
        /// <summary>
        /// 日志
        /// </summary>
        public ILogger Logger { get; set; }
        /// <summary>
        /// 在线客户端管理器
        /// </summary>

        private readonly IOnlineClientManager onlineClientManager;
        /// <summary>
        /// 通用Hub
        /// </summary>
        private static IHubContext CommonHub
        {
            get
            {
                return GlobalHost.ConnectionManager.GetHubContext<CommonHub>();
            }
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="onlineClientManager"></param>
        public SignalRRealTimeNotifier(IOnlineClientManager onlineClientManager)
        {
            this.onlineClientManager = onlineClientManager;
            Logger = NullLogger.Instance;
        }

        /// <summary>
        /// 发送消息
        /// </summary>
        /// <param name="userNotifications"></param>
        /// <returns></returns>
        public Task SendNotificationsAsync(List<UserNotification> userNotifications)
        {
            foreach (var userNotification in userNotifications)
            {
                try
                {
                    var onlineClient = onlineClientManager.GetByUserIdOrNull(userNotification.UserId);
                    if (onlineClient == null)
                    {
                        continue;
                    }

                    var signalRClient = CommonHub.Clients.Client(onlineClient.ConnectionId);
                    if (signalRClient == null)
                    {
                        Logger.Debug("Can not get user " + userNotification.UserId + " from SignalR hub!");
                        continue;
                    }

                    signalRClient.getNotification(userNotification);
                }
                catch (Exception ex)
                {
                    Logger.Warn("Could not send notification to userId: " + userNotification.UserId);
                    Logger.Warn(ex.ToString(), ex);
                }
            }

            return Task.FromResult(0);
        }

        public Task SendNotificationMessagesAsync(List<NotificationMessage> messages)
        {
            try
            {
                foreach (var onlineClient in this.onlineClientManager.GetAllClients())
                {
                    CommonHub.Clients.Client(onlineClient.ConnectionId).getNotificationMessage(messages);
                }
            }
            catch (Exception ex)
            {
                Logger.Warn(ex.ToString(), ex);
            }
            return Task.FromResult(0);
        }
    }
}
