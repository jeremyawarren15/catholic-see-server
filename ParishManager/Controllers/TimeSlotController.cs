using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParishManager.Core.Entities;
using ParishManager.Core.Models.TimeSlotModels;
using ParishManager.Models.TimeSlot;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Controllers
{
    [Authorize]
    public class TimeSlotController : Controller
    {
        private readonly ITimeSlotService _timeSlotService;
        private readonly ITimeSlotCommitmentService _commitmentService;
        private readonly ITimeService _timeService;
        private readonly UserManager<User> _userManager;

        public TimeSlotController(
            ITimeSlotService timeSlotService,
            ITimeSlotCommitmentService commitmentService,
            ITimeService timeService,
            UserManager<User> userManager)
        {
            _timeSlotService = timeSlotService;
            _commitmentService = commitmentService;
            _timeService = timeService;
            _userManager = userManager;
        }

        public IActionResult Index(int parishId = 1, string alertMessageText = null)
        {
            var userId = _userManager.GetUserId(User);

            var timeSlots = _timeSlotService.GetTimeSlotsByParishId(userId, parishId)
                .Select(x => new TimeSlotListItemViewModel()
                {
                    TimeSlotId = x.TimeSlotId,
                    Day = x.Day,
                    Hour = x.Hour,
                    Location = x.Location,
                    IsClaimedByUser = x.IsClaimedByUser
                });

            var viewModel = new TimeSlotIndexViewModel()
            {
                TimeSlots = timeSlots,
                AlertMessage = alertMessageText,
                ParishId = parishId
            };

            return View(viewModel);
        }

        public IActionResult Claim(int id)
        {
            var userId = _userManager.GetUserId(User);

            _commitmentService.Claim(userId, id);

            return RedirectToAction("Index", new { alertMessageText = "Hour successfully claimed!" });
        }

        public IActionResult Unclaim(int id)
        {
            var userId = _userManager.GetUserId(User);

            _commitmentService.Unclaim(userId, id);

            return RedirectToAction("Index", new { alertMessageText = "Hour successfully unclaimed!" });
        }

        public IActionResult Create(int parishId)
        {
            var hoursList = GetHoursList();

            var viewModel = new TimeSlotCreateViewModel
            {
                ParishId = parishId,
                HoursList = hoursList
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(TimeSlotCreateViewModel model)
        {
            var timeSlot = new TimeSlotCreate()
            {
                Day = model.Day,
                Hour = model.Hour,
                ParishId = model.ParishId,
                Location = model.Location
            };

            _timeSlotService.Create(timeSlot);

            return RedirectToAction("Index", new { parishId = model.ParishId, alertMessageText = "New time slot successfully created!" });
        }

        public IActionResult Edit(int id)
        {
            var timeSlot = _timeSlotService.Get(id);

            var committedUsers = _commitmentService
                .GetCommitedUsersForTimeSlot(id)
                .Select(x => new TimeSlotEditUserList()
                {
                    Name = x.UserName,
                    Email = x.Email
                });

            var viewModel = new TimeSlotEditModel()
            {
                Day = timeSlot.Day,
                HourText = _timeService.ConvertTimeToString(timeSlot.Hour),
                Enabled = true,
                Location = timeSlot.Location,
                TimeSlotId = id,
                MinimumRequiredAdorers = 2,
                CommittedAdorers = committedUsers
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(TimeSlotEditModel model)
        {
            var timeSlot = new TimeSlotUpdate()
            {
                Id = model.TimeSlotId,
                Location = model.Location
            };

            _timeSlotService.Update(timeSlot);

            return RedirectToAction("Index", new { alertMessageText = "Time slot successfully updated!" });
        }

        private IEnumerable<SelectListItem> GetHoursList()
        {
            return _timeService.GetAllHours()
                .Select((x, index) => new SelectListItem()
                {
                    Text = x,
                    Value = index.ToString()
                });
        }
    }
}
