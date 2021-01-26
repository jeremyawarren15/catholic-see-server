using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class UserRepository : Repository<User, string>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context)
            :base(context)
        {
        }
    }
}
