using ParishManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Contracts
{
    public interface ITimeSlotCommitmentRepository : IRepository<TimeSlotCommitment, int>
    {
        TimeSlotCommitment GetByUserAndTimeSlotId(string userId, int timeSlotId);
        IEnumerable<User> GetCommittedUsersForTimeSlot(int timeSlotId);
    }
}
