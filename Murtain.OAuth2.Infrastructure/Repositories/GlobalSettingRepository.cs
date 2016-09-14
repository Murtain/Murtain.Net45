using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.OAuth2.Domain.Repositories;
using Murtain.EntityFramework;
using Murtain.OAuth2.Domain.Entities;

namespace Murtain.OAuth2.Infrastructure.Repositories
{
    public class GlobalSettingRepository : Repository<GlobalSetting>, IGlobalSettingRepository
    {
        public GlobalSettingRepository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
