﻿using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class TimeSlotRepository : Repository<TimeSlot, int>, ITimeSlotRepository
    {
        public TimeSlotRepository(ApplicationDbContext context)
            : base(context)
        {
        }
    }
}
