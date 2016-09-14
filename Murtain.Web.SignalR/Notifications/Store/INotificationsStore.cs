using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Web.SignalR.Notifications.Models;

namespace Murtain.Web.SignalR.Notifications.Store
{
    public interface INotificationsStore
    {
        /// <summary>
        /// Adds a notification subscription.
        /// </summary>
        Task AddSubscriptionAsync(NotificationSubscription subscription);

        /// <summary>
        /// Deletes a notification subscription.
        /// </summary>
        Task DeleteSubscriptionAsync(string userAccount, string notificationName, string entityTypeName, string entityId);

        /// <summary>
        /// Adds a notification.
        /// </summary>
        Task AddNotificationAsync(Notification notification);

        /// <summary>
        /// Gets a notification by Id, or returns null if not found.
        /// </summary>
        Task<Notification> GetNotificationOrNullAsync(Guid notificationId);

        /// <summary>
        /// Adds a user notification.
        /// </summary>
        Task AddUserNotificationAsync(UserNotification userNotification);

        /// <summary>
        /// Gets subscriptions for a notification.
        /// </summary>
        Task<List<NotificationSubscription>> GetSubscriptionsAsync(string notificationName, string entityTypeName, string entityId);

        /// <summary>
        /// Gets subscriptions for a notification for specified tenant(s).
        /// </summary>
        Task<List<NotificationSubscription>> GetSubscriptionsAsync(int?[] tenantIds, string notificationName, string entityTypeName, string entityId);

        /// <summary>
        /// Gets subscriptions for a user.
        /// </summary>
        Task<List<NotificationSubscription>> GetSubscriptionsAsync(long userId);

        /// <summary>
        /// Checks if a user subscribed for a notification
        /// </summary>
        Task<bool> IsSubscribedAsync(string userAccount, string notificationName, string entityTypeName, string entityId);

        /// <summary>
        /// Updates a user notification state.
        /// </summary>
        Task UpdateUserNotificationStateAsync(Guid userNotificationId, UserNotificationState state);

        /// <summary>
        /// Updates all notification states for a user.
        /// </summary>
        Task UpdateAllUserNotificationStatesAsync(string userAccount, UserNotificationState state);

        /// <summary>
        /// Deletes a user notification.
        /// </summary>
        Task DeleteUserNotificationAsync(Guid userNotificationId);

        /// <summary>
        /// Deletes all notifications of a user.
        /// </summary>
        Task DeleteAllUserNotificationsAsync(string userAccount);

        /// <summary>
        /// Gets a user notification.
        /// </summary>
        /// <param name="userNotificationId">Skip count.</param>
        Task<UserNotification> GetUserNotificationIncludeNotificationOrNullAsync(Guid userNotificationId);
        /// <summary>
        /// Gets notifications of a user.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="skipCount">Skip count.</param>
        /// <param name="maxResultCount">Maximum result count.</param>
        /// <param name="state">State</param>
        Task<List<UserNotification>> GetUserNotificationsIncludeNotificationAsync(string userAccount, UserNotificationState? state = null, int skipCount = 0, int maxResultCount = int.MaxValue);

        /// <summary>
        /// Gets user notification count.
        /// </summary>
        /// <param name="userId">The user identifier.</param>
        /// <param name="state">The state.</param>
        Task<int> GetUserNotificationCountAsync(string userAccount, UserNotificationState? state = null);
    }
}
