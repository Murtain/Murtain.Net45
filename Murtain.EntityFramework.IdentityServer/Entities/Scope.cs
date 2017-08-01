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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityServer3.EntityFramework.Entities
{
    [Table("SCOPES")]
    public class Scope
    {
        [Key]
        [Column("ID")]
        public virtual int Id { get; set; }
        [Column("ENABLED")]
        public virtual bool Enabled { get; set; }
        
        [Required]
        [StringLength(200)]
        [Column("NAME")]
        public virtual string Name { get; set; }
        
        [StringLength(200)]
        [Column("DISPLAY_NAME")]
        public virtual string DisplayName { get; set; }
        
        [StringLength(1000)]
        [Column("DESCRIPTION")]
        public virtual string Description { get; set; }

        [Column("REQUIRED")]
        public virtual bool Required { get; set; }
        [Column("EMPHASIZE")]
        public virtual bool Emphasize { get; set; }
        [Column("TYPE")]
        public virtual int Type { get; set; }
        public virtual ICollection<ScopeClaim> ScopeClaims { get; set; }
        [Column("INCLUDE_ALL_CLAIMS_FOR_USER")]
        public virtual bool IncludeAllClaimsForUser { get; set; }
        public virtual ICollection<ScopeSecret> ScopeSecrets { get; set; }

        [StringLength(200)]
        [Column("CLAIMS_RULE")]
        public virtual string ClaimsRule { get; set; }

        [Column("SHOW_IN_DISCOVERY_DOCUMENT")]
        public virtual bool ShowInDiscoveryDocument { get; set; }
        [Column("ALLOW_UNRESTRICTED_INTROSPECTION")]
        public virtual bool AllowUnrestrictedIntrospection { get; set; }
    }
}
