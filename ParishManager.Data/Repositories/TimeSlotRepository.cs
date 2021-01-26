using ParishManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class TimeSlotRepository : Repository<TimeSlot, int>
    {
        public TimeSlotRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
