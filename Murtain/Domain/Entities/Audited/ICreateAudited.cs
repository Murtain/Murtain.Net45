using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Murtain.Domain.Entities.Audited
{
    public interface ICreateAudited
    {
        /// <summary>
        /// Creator user of this entity.
        /// </summary>
        string CreateUser { get; set; }
        /// <summary>
        /// Creation time of this entity.
        /// </summary>
        DateTime? CreateTime { get; set; }
    }
}
