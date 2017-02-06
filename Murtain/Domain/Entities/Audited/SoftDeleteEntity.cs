using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Entities.Audited
{
    public class SoftDeleteEntity : SoftDeleteEntity<long>
    {
    }

    public class SoftDeleteEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, ISoftDelete
    {
        /// <summary>
        /// Used to mark an Entity as 'Deleted'. 
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }
}
