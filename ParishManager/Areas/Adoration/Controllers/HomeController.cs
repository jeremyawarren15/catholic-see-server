using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParishManager.Areas.Adoration.Models.Home;
using ParishManager.Constants;
using ParishManager.Data.Entities;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.Adoration.Controllers
{
    [Authorize]
    [Area(AreaName.Adoration)]
    public class HomeController : Controller
    {
        private readonly ITimeSlotService _timeSlotService;
        private readonly ITimeSlotCommitmentService _commitmentService;
        private readonly ITimeService _timeService;
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly ISubstitutionRequestService _substitutionRequestService;

        public HomeController(
            ITimeSlotService timeSlotService,
            ITimeSlotCommitmentService commitmentService,
            ITimeService timeService,
            UserManager<User> userManager,
            IUserService userService,
            ISubstitutionRequestService substitutionRequestService)
        {
            _timeSlotService = timeSlotService;
            _timeService = timeService;
            _userManager = userManager;
            _userService = userService;
            _substitutionRequestService = substitutionRequestService;
            _commitmentService = commitmentService;
        }

        public IActionResult Index(int parishId = 1, string alertMessageText = null)
        {
            var userId = _userManager.GetUserId(User);
            var isAdmin = _userService.IsAdminForParish(userId, parishId);

            var commitments = _commitmentService.GetCommitments(userId)
                .Select(x => new ClaimedHoursListItemViewModel()
                { 
                    TimeSlotId = x.TimeSlotId,
                    Day = x.TimeSlot.Day,
                    Hour = _timeService.ConvertTimeToString(x.TimeSlot.Hour),
                    ParishName = x.TimeSlot.Parish.ParishName,
                    Location = x.TimeSlot.Location
                });

            var viewModel = new ClaimedHoursIndexViewModel()
            {
                MyHours = commitments,
                AlertMessage = alertMessageText,
                ParishId = parishId,
                IsAdmin = isAdmin,
            };

            return View(viewModel);
        }
    }
}
