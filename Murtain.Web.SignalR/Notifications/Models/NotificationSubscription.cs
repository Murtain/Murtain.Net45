using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Murtain.Domain.Entities.Audited;

namespace Murtain.Web.SignalR.Notifications.Models
{
    /// <summary>
    /// Used to store a notification subscription.
    /// </summary>
    [Table("NOTIFICATION_SUBSCRIPTION")]
    public class NotificationSubscription : CreateAuditedEntity<Guid>
    {
        /// <summary>
        /// User Account.
        /// </summary>
        [Column("USER_ACCOUNT")]
        public virtual string UserAccount { get; set; }

        /// <summary>
        /// Notification unique name.
        /// </summary>
        [MaxLength(96)]
        [Column("NOTIFICATION_NAME")]
        public virtual string NotificationName { get; set; }

        /// <summary>
        /// Gets/sets entity type name, if this is an entity level notification.
        /// It's FullName of the entity type.
        /// </summary>
        [MaxLength(250)]
        [Column("ENTITY_TYPE_NAME")]
        public virtual string EntityTypeName { get; set; }

        /// <summary>
        /// AssemblyQualifiedName of the entity type.
        /// </summary>
        [MaxLength(512)]
        [Column("ENTITY_TYPE_ASSEMBLY_QUALIFIED_NAME")]
        public virtual string EntityTypeAssemblyQualifiedName { get; set; }

        /// <summary>
        /// Gets/sets primary key of the entity, if this is an entity level notification.
        /// </summary>
        [MaxLength(96)]
        [Column("ENTITY_ID")]
        public virtual string EntityId { get; set; }

    }
}
