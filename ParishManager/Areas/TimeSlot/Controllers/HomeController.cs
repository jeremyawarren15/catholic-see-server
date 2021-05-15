using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ParishManager.Areas.TimeSlot.Models;
using ParishManager.Data.Entities;
using ParishManager.Data.Models.TimeSlotModels;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.TimeSlot.Controllers
{
    [Authorize]
    [Area("TimeSlot")]
    public class HomeController : Controller
    {
        private readonly ITimeSlotService _timeSlotService;
        private readonly ITimeSlotCommitmentService _commitmentService;
        private readonly ITimeService _timeService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public HomeController(
            ITimeSlotService timeSlotService,
            ITimeSlotCommitmentService commitmentService,
            ITimeService timeService,
            UserManager<User> userManager,
            IUserService userService)
        {
            _timeSlotService = timeSlotService;
            _commitmentService = commitmentService;
            _timeService = timeService;
            _userManager = userManager;
            _userService = userService;
        }

        public IActionResult Index(int parishId = 1, string alertMessageText = null)
        {
            var userId = _userManager.GetUserId(User);
            var isAdmin = _userService.IsAdminForParish(userId, parishId);

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
                ParishId = parishId,
                IsAdmin = isAdmin,
            };

            return View(viewModel);
        }

        public IActionResult Create(int parishId)
        {
            var userId = _userManager.GetUserId(User);
            var isAdmin = _userService.IsAdminForParish(userId, parishId);

            if (!isAdmin)
            {
                return RedirectToAction("Index", "TimeSlot", new { Area = "TimeSlot", parishId, alertMessageText = "Could not create time slot due to improper access rights." });
            }

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
            var userId = _userManager.GetUserId(User);
            var isAdmin = _userService.IsAdminForParish(userId, model.ParishId);

            if (!isAdmin)
            {
                return RedirectToAction("Index", new {
                    parishId = model.ParishId,
                    alertMessageText = "Could not create time slot due to improper access rights."
                });
            }

            var timeSlot = new TimeSlotCreate()
            {
                Day = model.Day,
                Hour = model.Hour,
                ParishId = model.ParishId,
                Location = model.Location,
                MinimumNumberOfAdorers = model.MinimumNumberOfAdorers,
                Enabled = model.Enabled
            };

            _timeSlotService.Create(timeSlot);

            return RedirectToAction("Index", new {
                parishId = model.ParishId,
                alertMessageText = "New time slot successfully created!"
            });
        }

        public IActionResult Edit(int id)
        {
            var timeSlot = _timeSlotService.Get(id);

            var userId = _userManager.GetUserId(User);
            var isAdmin = _userService.IsAdminForParish(userId, timeSlot.Parish.Id);

            if (!isAdmin)
            {
                return RedirectToAction("Index", new {
                    parishId = timeSlot.Parish.Id,
                    alertMessageText = "Could not edit time slot due to improper access rights."
                });
            }

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
                Enabled = timeSlot.Enabled,
                Location = timeSlot.Location,
                TimeSlotId = id,
                MinimumNumberOfAdorers = timeSlot.MinimumNumberOfAdorers,
                CommittedAdorers = committedUsers
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(TimeSlotEditModel model)
        {
            // really need to check if this user is authenticated
            // will require getting the time slot and then editing the time slot

            var timeSlot = new TimeSlotUpdate()
            {
                Id = model.TimeSlotId,
                Location = model.Location,
                Enabled = model.Enabled,
                MinimumNumberOfAdorers = model.MinimumNumberOfAdorers
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

        public IActionResult Delete(int id)
        {
            var timeSlot = _timeSlotService.Get(id);

            var userId = _userManager.GetUserId(User);
            var isAdmin = _userService.IsAdminForParish(userId, timeSlot.Parish.Id);

            if (!isAdmin)
            {
                return RedirectToAction("Index", new {
                    parishId = timeSlot.Parish.Id,
                    alertMessageText = "Could not delete time slot due to improper access rights."
                });
            }

            _timeSlotService.Delete(id);

            return RedirectToAction("Index", new { id = timeSlot.Parish.Id, alertMessageText = "Time slot successfully deleted!" });
        }
    }
}
