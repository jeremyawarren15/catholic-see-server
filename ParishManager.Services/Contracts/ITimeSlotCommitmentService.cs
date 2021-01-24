using ParishManager.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface ITimeSlotCommitmentService
    {
        TimeSlotCommitment CreateTimeSlotCommitment(int timeSlotId, string userId);
        ICollection<TimeSlotCommitment> GetAllTimeSlotCommitments();
        ICollection<TimeSlotCommitment> GetTimeSlotCommitmentsForUser(string userId);
        ICollection<TimeSlotCommitment> GetActiveTimeSlotCommitmentsForTimeSlot(int timeSlotId);
        TimeSlotCommitment GetTimeSlotCommitment(int timeSlotCommitmentId);
        bool IsTimeSlotCommitmentActive(int timeSlotCommitmentId);
        bool DeactivateTimeSlotCommitment(int timeSlotId);
    }
}
