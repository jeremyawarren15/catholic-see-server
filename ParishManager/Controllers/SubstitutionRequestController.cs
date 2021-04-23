using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ParishManager.Models.SubstitutionRequest;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Controllers
{
    public class SubstitutionRequestController : Controller
    {
        private readonly ITimeSlotService _timeSlotService;
        private readonly ITimeService _timeService;

        public SubstitutionRequestController(ITimeSlotService timeSlotService, ITimeService timeService)
        {
            _timeSlotService = timeSlotService;
            _timeService = timeService;
        }

        public ActionResult Create(int timeSlotId)
        {
            var timeSlot = _timeSlotService.Get(timeSlotId);

            var model = new SubstitutionRequestCreateViewModel()
            {
                ParishName = timeSlot.Parish.ParishName,
                DayOfWeek = timeSlot.Day,
                HourString = _timeService.ConvertTimeToString(timeSlot.Hour),
                TimeSlotId = timeSlotId,
            };
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
