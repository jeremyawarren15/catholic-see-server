using ParishManager.Data;
using ParishManager.Models.TimeSlotModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface ITimeSlotService
    {
        TimeSlot CreateTimeSlot(TimeSlotCreate createModel);
        ICollection<TimeSlot> GetAllTimeSlots();
        ICollection<TimeSlot> GetAllTimeSlotsForParish(int parishId);
        TimeSlot GetTimeSlot(int timeSlotId);
        TimeSlot UpdateTimeSlot(TimeSlotUpdate updateModel);
        bool DeleteTimeSlot(int timeSlotId);
    }
}
