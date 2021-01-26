using ParishManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class TimeSlotCommitmentRepository : Repository<TimeSlotCommitment, int>
    {
        public TimeSlotCommitmentRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
