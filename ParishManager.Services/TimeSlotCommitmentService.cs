using ParishManager.Data;
using ParishManager.Data.Entities;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services
{
    public class TimeSlotCommitmentService : ITimeSlotCommitmentService
    {
        private readonly ApplicationDbContext _context;

        public TimeSlotCommitmentService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Claim(string userId, int timeSlotId)
        {
            var commitment = new TimeSlotCommitment()
            {
                UserId = userId,
                TimeSlotId = timeSlotId,
                Active = true
            };

            _context.TimeSlotCommitments.Add(commitment);

            return _context.SaveChanges() != 0;
        }

        public IEnumerable<User> GetCommitedUsersForTimeSlot(int timeSlotId)
        {
            return _context.TimeSlots
                .SingleOrDefault(x => x.Id == timeSlotId)
                .TimeSlotCommitments
                .Select(x => x.User);
        }

        public bool Unclaim(string user, int timeSlot)
        {
            var commitment = _context.TimeSlotCommitments
                .SingleOrDefault(x => x.UserId == user && x.TimeSlotId == timeSlot);

            if (commitment == null)
            {
                return false;
            }

            _context.TimeSlotCommitments.Remove(commitment);

            return _context.SaveChanges() != 0;
        }

        public IEnumerable<TimeSlotCommitment> GetCommitments(string userId)
        {
            var commitments = _context.TimeSlotCommitments
                .Where(x => x.UserId == userId);

            return commitments;
        }
    }
}
