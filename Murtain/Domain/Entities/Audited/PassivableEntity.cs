using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Entities.Audited
{
    public class PassivableEntity : PassivableEntity<long>
    {
    }

    public class PassivableEntity<TPrimaryKey> : AuditedEntity<TPrimaryKey>, IPassivable
    {
        /// <summary>
        /// True: This entity is active.
        /// False: This entity is not active.
        /// </summary>
        public virtual bool IsActived { get; set; }
    }
}
