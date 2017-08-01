﻿/*
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityServer3.EntityFramework.Entities
{

    [Table("CORS_ORIGINS")]
    public class ClientCorsOrigin
    {
        [Key]
        [Column("ID")]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(150)]
        [Column("ORIGIN")]
        public virtual string Origin { get; set; }

        [Column("CLIENT_ID")]
        public int ClientId { get; set; }
        public virtual Client Client { get; set; }
    }
}
