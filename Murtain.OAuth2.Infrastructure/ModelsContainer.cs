using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Configuration;
using System.Data.Entity.Infrastructure.Interception;

using Murtain.EntityFramework;
using Murtain.OAuth2.Domain;
using Murtain.OAuth2.Domain.Entities;

namespace Murtain.OAuth2.Infrastructure
{

    public partial class ModelsContainer : EntityFrameworkDbContext
    {
        public ModelsContainer()
            : base("DefaultConnection")
        {

        }
        public ModelsContainer(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            this.Configuration.LazyLoadingEnabled = false;
            DbInterception.Add(new EntityFrameworkDbCommandInterceptor());
        }


        public virtual DbSet<UserAccount> UserAccount { get; set; }
        public virtual DbSet<GlobalSetting> GlobalSetting { get; set; }
    }


}
