using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Entities.Audited
{
    public class PassivableSoftDeleteEntity : PassivableSoftDeleteEntity<long>
    {
    }

    public class PassivableSoftDeleteEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IPassivable, ISoftDelete
    {
        /// <summary>
        /// True: This entity is active.
        /// False: This entity is not active.
        /// </summary>
        public virtual bool IsActived { get; set; }

        /// <summary>
        /// True: This entity is active.
        /// False: This entity is not active.
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }

}
