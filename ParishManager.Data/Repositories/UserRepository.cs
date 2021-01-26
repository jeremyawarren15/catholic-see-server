using ParishManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class UserRepository : Repository<User, string>
    {
        public UserRepository(ApplicationDbContext context)
            :base(context)
        {
        }
    }
}
