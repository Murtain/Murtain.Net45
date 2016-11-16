using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Entities.Audited
{
    public class CreateAuditedEntity : CreateAuditedEntity<long>
    {
    }

    public class CreateAuditedEntity<TPrimaryKey> : Entity<TPrimaryKey>, ICreateAudited
    {
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        [Column("CREATED_TIME")]
        public virtual DateTime? CreateTime { get; set; }

        /// <summary>
        /// Creator of this entity.
        /// </summary>
        [MaxLength(50)]
        [Column("CREATED_USER")]
        public virtual string CreateUser { get; set; }
    }
}
