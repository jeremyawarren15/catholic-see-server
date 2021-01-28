using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services
{
    public class TimeSlotCommitmentService : ITimeSlotCommitmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TimeSlotCommitmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Claim(string userId, int timeSlotId)
        {
            var commitment = new TimeSlotCommitment()
            {
                User = _unitOfWork.Users.Get(userId),
                TimeSlot = _unitOfWork.TimeSlots.Get(timeSlotId),
                Active = true
            };

            _unitOfWork.TimeSlotCommitments.Add(commitment);

            return _unitOfWork.Complete() != 0;
        }

        public bool Unclaim(string user, int timeSlot)
        {
            var commitment = _unitOfWork.TimeSlotCommitments
                .GetByUserAndTimeSlotId(user, timeSlot);

            _unitOfWork.TimeSlotCommitments.Remove(commitment);

            return _unitOfWork.Complete() != 0;
        }
    }
}
