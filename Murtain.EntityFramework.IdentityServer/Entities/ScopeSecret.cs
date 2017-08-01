/*
 * Copyright 2014 Dominick Baier, Brock Allen
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer3.EntityFramework.Entities
{
    [Table("SCOPE_SECRETS")]
    public class ScopeSecret
    {
        [Key]
        [Column("ID")]
        public virtual int Id { get; set; }

        [StringLength(1000)]
        [Column("DESCRIPTION")]
        public virtual string Description { get; set; }

        [Column("EXPIRATION")]
        public virtual DateTime? Expiration { get; set; }

        [StringLength(250)]
        [Column("TYPE")]
        public virtual string Type { get; set; }

        [Required]
        [StringLength(250)]
        [Column("VALUE")]
        public virtual string Value { get; set; }

        [Column("SCOPE_ID")]
        public int ScopeId { get; set; }
        public virtual Scope Scope { get; set; }
    }
}
