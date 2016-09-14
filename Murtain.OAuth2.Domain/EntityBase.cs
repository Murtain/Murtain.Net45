using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Murtain.Domain.Entities.Audited;
using Murtain.Domain.Entities;

namespace Murtain.OAuth2.Domain
{
    public abstract class AuditedEntityBase : AuditedEntity
    {
        [Column("ID")]
        public override long Id { get; set; }
        [Column("CREATED_TIME")]
        public override DateTime? CreateTime { get; set; }
        [MaxLength(50)]
        [Column("CREATED_USER")]
        public override string CreateUser { get; set; }
        [MaxLength(50)]
        [Column("CHANGED_USER")]
        public override string ChangeUser { get; set; }
        [Column("CHANGED_TIME")]
        public override DateTime? ChangeTime { get; set; }
    }

    public abstract class PassivableEntityBase : AuditedEntityBase, IPassivable
    {
        [Column("IS_ACTIVED")]
        public virtual bool IsActive { get; set; }
    }


    public abstract class SoftDeleteEntityBase : AuditedEntityBase, ISoftDelete
    {
        [Column("IS_DELETED")]
        public virtual bool IsDeleted { get; set; }
    }

    public abstract class PassivableSoftDeleteEntityBase : AuditedEntityBase, IPassivable, ISoftDelete
    {
        [Column("IS_ACTIVED")]
        public virtual bool IsActive { get; set; }
        [Column("IS_DELETED")]
        public virtual bool IsDeleted { get; set; }
    }
}
