using ParishManager.Core.Entities;
using ParishManager.Core.Models.TimeSlotModels;
using ParishManager.Data.Contracts;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services
{
    public class TimeSlotService : ITimeSlotService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITimeFormatterService _timeService;

        public TimeSlotService(IUnitOfWork unitOfWork, ITimeFormatterService timeService)
        {
            _unitOfWork = unitOfWork;
            _timeService = timeService;
        }

        public TimeSlot Create(TimeSlot entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(TimeSlot entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public TimeSlot Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeSlot> GetAll()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TimeSlotListItem> GetTimeSlotsByParishId(string userId, int parishId)
        {
            return _unitOfWork.TimeSlots
                .GetTimeSlotsByParishId(parishId)
                .Select(x => new TimeSlotListItem()
                {
                    TimeSlotId = x.Id,
                    Day = x.Day,
                    Hour = _timeService.ConvertTimeToString(x.Hour),
                    Location = x.Location,
                    IsClaimedByUser = x.TimeSlotCommitments.Any(x => x.User.Id == userId)
                });
        }

        public TimeSlot Update(TimeSlot entity)
        {
            throw new NotImplementedException();
        }
    }
}
