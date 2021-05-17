﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParishManager.Areas.SubstitutionRequest.Models.Home;
using ParishManager.Constants;
using ParishManager.Data.Entities;
using ParishManager.Data.Models.SubstitutionRequest;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.SubstitutionRequest.Controllers
{
    [Area(AreaName.SubstitutionRequest)]
    public class HomeController : Controller
    {
        private readonly ITimeSlotService _timeSlotService;
        private readonly ITimeService _timeService;
        private readonly ISubstitutionRequestService _substitutionRequestService;
        private readonly UserManager<User> _userManager;

        public HomeController(
            ITimeSlotService timeSlotService,
            ITimeService timeService,
            ISubstitutionRequestService substitutionRequestService,
            UserManager<User> userManager)
        {
            _timeSlotService = timeSlotService;
            _timeService = timeService;
            _substitutionRequestService = substitutionRequestService;
            _userManager = userManager;
        }

        // id is the time slot id
        public ActionResult Create(int id)
        {
            var timeSlot = _timeSlotService.Get(id);

            var upcomingDates = GetUpcomingDates(timeSlot.Day);

            var model = new SubstitutionRequestCreateViewModel()
            {
                ParishName = timeSlot.Parish.ParishName,
                DayOfWeek = timeSlot.Day,
                HourString = _timeService.ConvertTimeToString(timeSlot.Hour),
                TimeSlotId = id,
                UpcomingDates = upcomingDates
            };

            return View(model);
        }

        private IEnumerable<SelectListItem> GetUpcomingDates(DayOfWeek day)
        {
            // Gets next 10 valid dates
            var dates = _timeService.GetUpcomingDates(day, 10)
                .Select(x => new SelectListItem()
                {
                    Text = x.ToShortDateString(),
                    Value = x.ToShortDateString()
                });

            return dates;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SubstitutionRequestCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var createModel = new SubstitutionRequestCreate()
            {
                TimeSlotId = model.TimeSlotId,
                DateOfSubstitution = model.DateOfSubstitution,
                UserId = _userManager.GetUserId(User)
            };

            var wasCreated = _substitutionRequestService.Create(createModel);

            // TODO: need to add the temp message to display

            return RedirectToAction("Index", "Home", new { Area = AreaName.TimeSlot });
        }
    }
}
