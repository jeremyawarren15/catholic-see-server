using CatholicSee.Data;
using CatholicSee.Data.Entities;
using CatholicSee.Data.Models.TimeSlotModels;
using CatholicSee.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatholicSee.Services
{
    public class TimeSlotService : ServiceBase<TimeSlot, int>, ITimeSlotService
    {
        private readonly ApplicationDbContext _context;
        private readonly ITimeService _timeService;

        public TimeSlotService(ApplicationDbContext context, ITimeService timeService) : base(context)
        {
            _context = context;
            _timeService = timeService;
        }

        public TimeSlot Create(TimeSlotCreate model)
        {
            var entity = new TimeSlot()
            {
                ParishId = model.ParishId,
                Day = model.Day,
                Hour = model.Hour,
                Location = model.Location,
                MinimumNumberOfAdorers = model.MinimumNumberOfAdorers
            };

            var timeSlot = _context.TimeSlots
                .Add(entity)
                .Entity;

            _context.SaveChanges();

            return timeSlot;
        }

        public IEnumerable<TimeSlotListItem> GetTimeSlotsByParishId(string userId, int parishId)
        {
            // The reason userId is necessary is because this is supposed to
            // tell you which of the time slots for the parish are clamied by
            // the given user. The name of this method may not be the best.
            return _context.TimeSlots
                .Where(x => x.ParishId == parishId)
                .Select(x => new TimeSlotListItem()
                {
                    TimeSlotId = x.Id,
                    Day = x.Day,
                    Hour = _timeService.ConvertTimeToString(x.Hour),
                    Location = x.Location,
                    IsClaimedByUser = x.TimeSlotCommitments.Any(x => x.User.Id == userId),
                    CommittedAdorers = x.TimeSlotCommitments.Count,
                    MinimumAdorers = x.MinimumNumberOfAdorers
                });
        }

        public TimeSlot Update(TimeSlotUpdate model)
        {
            var timeSlot = _context.TimeSlots
                .SingleOrDefault(x => x.Id == model.Id);

            timeSlot.Location = model.Location;
            timeSlot.Enabled = model.Enabled;
            if (model.MinimumNumberOfAdorers >= 0)
            {
                timeSlot.MinimumNumberOfAdorers = model.MinimumNumberOfAdorers;
            }

            _context.SaveChanges();

            return timeSlot;
        }
    }
}
