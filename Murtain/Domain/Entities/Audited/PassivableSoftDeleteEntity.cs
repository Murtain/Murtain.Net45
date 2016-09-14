using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Entities.Audited
{
    public class PassivableSoftDeleteEntity : AuditedEntity,IPassivable,ISoftDelete
    {
        /// <summary>
        /// True: This entity is active.
        /// False: This entity is not active.
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// True: This entity is active.
        /// False: This entity is not active.
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }

    public class PassivableSoftDeleteEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IPassivable, ISoftDelete
    {
        /// <summary>
        /// True: This entity is active.
        /// False: This entity is not active.
        /// </summary>
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// True: This entity is active.
        /// False: This entity is not active.
        /// </summary>
        public virtual bool IsDeleted { get; set; }
    }

}
