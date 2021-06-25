using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParishManager.Areas.Adoration.Models.Substitution;
using ParishManager.Constants;
using ParishManager.Data.Entities;
using ParishManager.Data.Models.SubstitutionRequest;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.Adoration.Controllers
{
    [Area(AreaName.Adoration)]
    [Authorize]
    public class SubstitutionController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly ISubstitutionRequestService _substitutionRequestService;
        private readonly ITimeService _timeService;
        private readonly ITimeSlotService _timeSlotService;

        public SubstitutionController(
            UserManager<User> userManager,
            IUserService userService,
            ISubstitutionRequestService substitutionRequestService,
            ITimeService timeService,
            ITimeSlotService timeSlotService)
        {
            _userManager = userManager;
            _userService = userService;
            _substitutionRequestService = substitutionRequestService;
            _timeService = timeService;
            _timeSlotService = timeSlotService;
        }

        public IActionResult Index(int parishId = 1, string alertMessageText = null)
        {
            var userId = _userManager.GetUserId(User);
            var isAdmin = _userService.IsAdminForParish(userId, parishId);

            var availableSubstitutions = _substitutionRequestService
                .GetUnclaimedSubstitutionRequests(parishId)
                .Select(x => new UnclaimedSubstitutionListItemViewModel()
                {
                    TimeSlotId = x.TimeSlotCommitment.TimeSlotId,
                    Day = x.TimeSlotCommitment.TimeSlot.Day,
                    Hour = _timeService.ConvertTimeToString(x.TimeSlotCommitment.TimeSlot.Hour),
                    Location = x.TimeSlotCommitment.TimeSlot.Location,
                    DateOfSubstitution = x.DateOfSubstitution,
                    SubstitutionId = x.Id
                });

            var model = new SubstitutionIndexViewModel()
            {
                ParishId = parishId,
                UnclaimedSubstitutions = availableSubstitutions,
                IsAdmin = isAdmin
            };

            return View(model);
        }

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

            return RedirectToAction("Index", "Home", new { Area = AreaName.Adoration });
        }

        public ActionResult PickUpHour(int substitutionId)
        {
            var userId = _userManager.GetUserId(User);
            _substitutionRequestService.PickUpHour(substitutionId, userId);

            return RedirectToAction("Index", "Substitution", new { Area = AreaName.Adoration });
        }
    }
}
