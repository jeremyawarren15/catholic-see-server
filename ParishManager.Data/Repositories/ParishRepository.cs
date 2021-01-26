using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class ParishRepository : Repository<Parish, int>, IParishRepository
    {
        public ParishRepository(ApplicationDbContext context)
            :base(context)
        {

        }
    }
}
