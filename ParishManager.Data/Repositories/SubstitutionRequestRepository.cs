using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class SubstitutionRequestRepository : Repository<SubstitutionRequest, int>, ISubstitutionRequestRepository
    {
        public SubstitutionRequestRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
