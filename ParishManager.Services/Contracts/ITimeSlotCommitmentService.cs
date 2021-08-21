using ParishManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParishManager.Services.Contracts
{
    public interface ITimeSlotCommitmentService
    {
        Task<bool> ClaimAsync(string user, int timeSlot);
        Task<bool> UnclaimAsync(string user, int timeSlot);
        IEnumerable<User> GetCommitedUsersForTimeSlot(int timeSlotId);
        IEnumerable<TimeSlotCommitment> GetCommitments(string userId);
    }
}
