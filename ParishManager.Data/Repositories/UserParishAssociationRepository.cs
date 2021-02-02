﻿using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class UserParishAssociationRepository : Repository<UserParishAssociation, int, string>, IUserParishAssociationRepository
    {
        public UserParishAssociationRepository(ApplicationDbContext context)
            :base(context)
        {
        }
    }
}
