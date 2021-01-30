using Microsoft.EntityFrameworkCore;
using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class TimeSlotCommitmentRepository : Repository<TimeSlotCommitment, int>, ITimeSlotCommitmentRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeSlotCommitmentRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }

        public TimeSlotCommitment GetByUserAndTimeSlotId(string userId, int timeSlotId)
        {
            return _context.TimeSlotCommitments
                .FirstOrDefault(x => x.User.Id == userId && x.TimeSlot.Id == timeSlotId);
        }

        public IEnumerable<User> GetCommittedUsersForTimeSlot(int timeSlotId)
        {
            return _context.TimeSlotCommitments
                .Include(x => x.User)
                .Where(x => x.TimeSlot.Id == timeSlotId)
                .Select(x => x.User)
                .ToList();
        }
    }
}
