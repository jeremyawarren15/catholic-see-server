using CatholicSee.Data.Entities;
using CatholicSee.Data.Models.TimeSlotModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Services.Contracts
{
    public interface ITimeSlotService : IService<TimeSlot, int>
    {
        IEnumerable<TimeSlotListItem> GetTimeSlotsByParishId(string userId, int parishId);
        TimeSlot Create(TimeSlotCreate model);
        TimeSlot Update(TimeSlotUpdate model);
    }
}
