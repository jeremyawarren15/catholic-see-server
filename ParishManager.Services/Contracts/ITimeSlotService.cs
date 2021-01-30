using ParishManager.Core.Entities;
using ParishManager.Core.Models.TimeSlotModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface ITimeSlotService : IService<TimeSlot, int>
    {
        IEnumerable<TimeSlotListItem> GetTimeSlotsByParishId(string userId, int parishId);
        TimeSlot Create(TimeSlotCreate model);
        TimeSlot Update(TimeSlotUpdate model);
    }
}
