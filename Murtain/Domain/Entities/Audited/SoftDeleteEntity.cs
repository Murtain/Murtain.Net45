using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Entities.Audited
{
    public class SoftDeleteEntity : AuditedEntity, ISoftDelete
    {
        /// <summary>
        /// Used to mark an Entity as 'Deleted'. 
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }

    public class SoftDeleteEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, ISoftDelete
    {
        /// <summary>
        /// Used to mark an Entity as 'Deleted'. 
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }
}
