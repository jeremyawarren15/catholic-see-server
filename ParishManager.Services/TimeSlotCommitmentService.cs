using Microsoft.AspNetCore.Identity;
using ParishManager.Data;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services
{
    public class TimeSlotCommitmentService : ITimeSlotCommitmentService
    {
        public readonly ApplicationDbContext _context;
        public readonly ITimeSlotService _timeSlotService;

        public TimeSlotCommitmentService(ApplicationDbContext context, ITimeSlotService timeSlotService)
        {
            _context = context;
            _timeSlotService = timeSlotService;
        }

        public TimeSlotCommitment CreateTimeSlotCommitment(int timeSlotId, string userId)
        {
            var commitment = new TimeSlotCommitment()
            {
                TimeSlot = _timeSlotService.GetTimeSlot(timeSlotId),
                User = _context.Users.SingleOrDefault(x => x.Id == userId), // need to change this with the userService (when it's created)
                Active = true
            };

            var newCommitment = _context.TimeSlotCommitments
                .Add(commitment);

            _context.SaveChanges();

            return newCommitment.Entity;
        }

        public bool DeactivateTimeSlotCommitment(int timeSlotCommitmentId)
        {
            var commitment = GetTimeSlotCommitment(timeSlotCommitmentId);

            commitment.Active = false;

            return _context.SaveChanges() != 0;
        }

        public ICollection<TimeSlotCommitment> GetAllTimeSlotCommitments()
        {
            return _context.TimeSlotCommitments
                .ToList();
        }

        public TimeSlotCommitment GetTimeSlotCommitment(int timeSlotCommitmentId)
        {
            return _context.TimeSlotCommitments
                .SingleOrDefault(x => x.Id == timeSlotCommitmentId);
        }

        public ICollection<TimeSlotCommitment> GetActiveTimeSlotCommitmentsForTimeSlot(int timeSlotId)
        {
            return _context.TimeSlotCommitments
                .Where(x => x.TimeSlot.Id == timeSlotId && x.Active)
                .ToList();
        }

        public ICollection<TimeSlotCommitment> GetTimeSlotCommitmentsForUser(string userId)
        {
            return _context.TimeSlotCommitments
                .Where(x => x.User.Id == userId)
                .ToList();
        }

        public bool IsTimeSlotCommitmentActive(int timeSlotCommitmentId)
        {
            return GetTimeSlotCommitment(timeSlotCommitmentId).Active;
        }
    }
}
