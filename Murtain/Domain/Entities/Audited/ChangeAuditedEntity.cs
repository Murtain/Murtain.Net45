using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Entities.Audited
{
    public class ChangeAuditedEntity : ChangeAuditedEntity<long>
    {
    }

    public class ChangeAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, IChangeAudited
    {
        /// <summary>
        /// The  modify user for this entity.
        /// </summary>
        [MaxLength(50)]
        public virtual string ChangeUser { get; set; }
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        public virtual DateTime? ChangeTime { get; set; }
    }
}
