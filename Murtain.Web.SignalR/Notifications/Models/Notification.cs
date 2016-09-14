using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.Domain.Entities.Audited;

namespace Murtain.Web.SignalR.Notifications.Models
{
    /// <summary>
    /// Represents a published notification.
    /// </summary>
    [Serializable]
    [Table("NOTIFICATION")]
    public class Notification : AuditedEntity<Guid>
    {
        public Notification()
        {
            UserNotifications = new HashSet<UserNotification>();
        }
        /// <summary>
        /// Unique notification name.
        /// </summary>
        [MaxLength(96)]
        [Column("NOTIFICATION_NAME")]
        public string NotificationName { get; set; }

        /// <summary>
        /// Gets or sets the type of the entity.
        /// </summary>
        [Column("ENTITY_TYPE")]
        public string EntityType { get; set; }

        /// <summary>
        /// Name of the entity type (including namespaces).
        /// </summary>

        [MaxLength(96)]
        [Column("ENTITY_TYPE_NAME")]
        public string EntityTypeName { get; set; }

        /// <summary>
        /// Entity id.
        /// </summary>
        [Column("ENTITY_ID")]
        public string EntityId { get; set; }

        /// <summary>
        /// Severity.
        /// </summary>
        [Column("SEVERITY")]
        public int Severity { get; set; }

        public virtual ICollection<UserNotification> UserNotifications { get; set; }

        /// <summary>
        /// Notification data.
        /// </summary>
        [NotMapped]
        public NotificationData Data { get; set; }

    }
}
