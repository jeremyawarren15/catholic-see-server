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
        private readonly ITimeService _timeService;

        public TimeSlotService(IUnitOfWork unitOfWork, ITimeService timeService)
        {
            _unitOfWork = unitOfWork;
            _timeService = timeService;
        }

        public TimeSlot Create(TimeSlot entity)
        {
            var timeSlot = _unitOfWork.TimeSlots.Add(entity);

            _unitOfWork.Complete();

            return timeSlot;
        }

        public TimeSlot Create(TimeSlotCreate model)
        {
            var parish = _unitOfWork.Parishes.Get(model.ParishId);

            var entity = new TimeSlot()
            {
                Parish = parish,
                Day = model.Day,
                Hour = model.Hour,
                Location = model.Location
            };

            var timeSlot = _unitOfWork.TimeSlots.Add(entity);

            _unitOfWork.Complete();

            return timeSlot;
        }

        public bool Delete(int id)
        {
            var timeSlot = _unitOfWork.TimeSlots.Get(id);

            _unitOfWork.TimeSlots.Remove(timeSlot);

            return _unitOfWork.Complete() != 0;
        }

        public TimeSlot Get(int id)
        {
            return _unitOfWork.TimeSlots.Get(id);
        }

        public IEnumerable<TimeSlot> GetAll()
        {
            return _unitOfWork.TimeSlots.GetAll();
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

        public TimeSlot Update(TimeSlotUpdate model)
        {
            var timeSlot = _unitOfWork.TimeSlots.Get(model.Id);

            timeSlot.Location = model.Location;

            _unitOfWork.Complete();

            return timeSlot;
        }
    }
}
