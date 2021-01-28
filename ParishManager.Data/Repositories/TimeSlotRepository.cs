using Microsoft.EntityFrameworkCore;
using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class TimeSlotRepository : Repository<TimeSlot, int>, ITimeSlotRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeSlotRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public IEnumerable<TimeSlot> GetTimeSlotsByParishId(int parishId)
        {
            return _context.Parishes
                .Include(x => x.TimeSlots)
                .ThenInclude(x => x.TimeSlotCommitments)
                .ThenInclude(x => x.User)
                .SingleOrDefault(x => x.Id == parishId)
                .TimeSlots
                .OrderBy(x => x.Day)
                .ThenBy(x => x.Hour);
        }
    }
}
