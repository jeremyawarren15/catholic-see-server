using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class TimeSlotCommitmentRepository : Repository<TimeSlotCommitment, int>, ITimeSlotCommitmentRepository
    {
        public TimeSlotCommitmentRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
