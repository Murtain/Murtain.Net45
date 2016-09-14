using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Entities
{
    /// <summary>
    /// This interface is implemented by entities which must be audited.
    /// Related properties automatically set when saving/updating <see cref="Entity"/> objects.
    /// </summary>
    public interface IAudited 
    {
        /// <summary>
        /// Creator user of this entity.
        /// </summary>
        string CreateUser { get; set; }
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        DateTime? CreateTime { get; set; }
        /// <summary>
        /// The  modify user for this entity.
        /// </summary>
        string ChangeUser { get; set; }
        /// <summary>
        /// The last modified time for this entity.
        /// </summary>
        DateTime? ChangeTime { get; set; }

    }
}
