using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Murtain.OAuth2.Domain;
using Murtain.EntityFramework;
using Murtain.OAuth2.Domain.Entities;
using Murtain.OAuth2.Domain.Repositories;

namespace Murtain.OAuth2.Infrastructure.Repositories
{
    public class UserAccountRepository : Repository<UserAccount>, IUserAccountRepository
    {
        public UserAccountRepository(IEntityFrameworkDbContextProvider<ModelsContainer> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
