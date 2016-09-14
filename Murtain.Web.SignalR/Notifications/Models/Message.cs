using Murtain.Domain.Entities.Audited;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Web.SignalR.Notifications.Models
{
    /// <summary>
    /// Represents a published message.
    /// </summary>
    [Serializable]
    [Table("NOTIFICATION_MESSAGE")]
    public class NotificationMessage : AuditedEntity<Guid>
    {
        /// <summary>
        /// Message title
        /// </summary>
        [Column("TITLE")]
        [MaxLength(96)]
        public string Title { get; set; }
        /// <summary>
        /// Message content
        /// </summary>
        [Column("CONTENT")]
        [MaxLength(2000)]
        public string Content { get; set; }

        [Column("DURATION")]
        [MaxLength(50)]
        public string Duration { get; set; }
    }
}
