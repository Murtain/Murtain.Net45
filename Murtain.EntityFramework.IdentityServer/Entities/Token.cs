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
    [Table("TOKENS")]
    public class Token
    {
        [Key, Column("KEY",Order = 0)]
        public virtual string Key { get; set; }

        [Key, Column("TOKEN_TYPE", Order = 1)]
        public virtual TokenType TokenType { get; set; }

        [StringLength(200)]
        [Column("SUBJECT_ID")]
        public virtual string SubjectId { get; set; }
        
        [Required]
        [StringLength(200)]
        [Column("CLIENT_ID")]
        public virtual string ClientId { get; set; }
        
        [Required]
        [DataType(DataType.Text)]
        [Column("JSON_CODE")]
        public virtual string JsonCode { get; set; }

        [Required]
        [Column("EXPIRY")]
        public virtual DateTime Expiry { get; set; }
    }
}
