using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Entities.Audited;

namespace Murtain.Web.SignalR.Notifications.Models
{
    /// <summary>
    /// Used to store a user notification.
    /// </summary>
    [Serializable]
    [Table("NOTIFICATION_USER")]
    public class UserNotification : CreateAuditedEntity<Guid>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserNotificationInfo"/> class.
        /// </summary>
        public UserNotification()
        {
            State = 0;
        }
        /// <summary>
        /// User Account.
        /// </summary>
        [Column("USER_ID")]
        public virtual string UserId { get; set; }

        /// <summary>
        /// Notification Id.
        /// </summary>
        [Required]
        [Column("NOTIFICATION_ID")]
        public virtual Guid NotificationId { get; set; }

        /// <summary>
        /// Current state of the user notification.
        /// </summary>
        [Column("STATE")]
        public virtual int State { get; set; }

        
        [ForeignKey("NotificationId")]
        public virtual Notification Notification { get; set; }
    }
}
