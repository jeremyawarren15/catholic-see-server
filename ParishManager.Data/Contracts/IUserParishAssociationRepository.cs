using ParishManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Contracts
{
    public interface IUserParishAssociationRepository : IRepository<UserParishAssociation, User, Parish>
    {
    }
}
