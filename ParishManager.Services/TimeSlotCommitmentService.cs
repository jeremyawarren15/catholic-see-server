using ParishManager.Data;
using ParishManager.Data.Entities;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ParishManager.Services
{
    public class TimeSlotCommitmentService : ITimeSlotCommitmentService
    {
        private readonly ApplicationDbContext _context;

        public TimeSlotCommitmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ClaimAsync(string userId, int timeSlotId)
        {
            var anyPreviousCommitments = _context.TimeSlotCommitments
                .Any(x => x.UserId == userId && x.TimeSlotId == timeSlotId);

            if (anyPreviousCommitments)
            {
                return false;
            }

            var commitment = new TimeSlotCommitment()
            {
                UserId = userId,
                TimeSlotId = timeSlotId,
                Active = true,
                CreatedById = userId
            };

            await _context.TimeSlotCommitments.AddAsync(commitment);

            return await _context.SaveChangesAsync() != 0;
        }

        public IEnumerable<User> GetCommitedUsersForTimeSlot(int timeSlotId)
        {
            return _context.TimeSlots
                .SingleOrDefault(x => x.Id == timeSlotId)
                .TimeSlotCommitments
                .Select(x => x.User);
        }

        public async Task<bool> UnclaimAsync(string user, int timeSlot)
        {
            var commitment = _context.TimeSlotCommitments
                .SingleOrDefault(x => x.UserId == user && x.TimeSlotId == timeSlot);

            if (commitment == null)
            {
                return false;
            }

            _context.TimeSlotCommitments.Remove(commitment);

            return await _context.SaveChangesAsync() != 0;
        }

        public IEnumerable<TimeSlotCommitment> GetCommitments(string userId)
        {
            var commitments = _context.TimeSlotCommitments
                .Where(x => x.UserId == userId);

            return commitments;
        }
    }
}
