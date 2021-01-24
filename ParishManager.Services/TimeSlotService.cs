using ParishManager.Data;
using ParishManager.Models.TimeSlotModels;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly ApplicationDbContext _context;

        public TimeSlotService(ApplicationDbContext context)
        {
            _context = context;
        }

        public TimeSlot CreateTimeSlot(TimeSlotCreate createModel)
        {
            var newTimeSlot = new TimeSlot()
            {
                Location = createModel.Location,
                Day = createModel.Day,
                Hour = createModel.Hour,
                Parish = createModel.Parish
            };

            var createdTimeSlot = _context.TimeSlots
                .Add(newTimeSlot);

            _context.SaveChanges();

            return createdTimeSlot.Entity;
        }

        public bool DeleteTimeSlot(int timeSlotId)
        {
            var timeSlotToDelete = _context.TimeSlots
                .SingleOrDefault(x => x.Id == timeSlotId);

            _context.TimeSlots
                .Remove(timeSlotToDelete);

            return _context.SaveChanges() != 0;
        }

        public ICollection<TimeSlot> GetAllTimeSlots()
        {
            return _context.TimeSlots
                .ToList();
        }

        public ICollection<TimeSlot> GetAllTimeSlotsForParish(int parishId)
        {
            return _context.TimeSlots
                .Where(x => x.Id == parishId)
                .ToList();
        }

        public TimeSlot GetTimeSlot(int timeSlotId)
        {
            return _context.TimeSlots
                .SingleOrDefault(x => x.Id == timeSlotId);
        }

        public TimeSlot UpdateTimeSlot(TimeSlotUpdate updateModel)
        {
            throw new NotImplementedException();
        }
    }
}
