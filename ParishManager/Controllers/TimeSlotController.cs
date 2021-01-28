using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParishManager.Core.Entities;
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
        private readonly UserManager<User> _userManager;

        public TimeSlotController(ITimeSlotService timeSlotService, ITimeSlotCommitmentService commitmentService, UserManager<User> userManager)
        {
            _timeSlotService = timeSlotService;
            _commitmentService = commitmentService;
            _userManager = userManager;
        }

        public IActionResult Index(string alertMessageText = null)
        {
            var userId = _userManager.GetUserId(User);

            var timeSlots = _timeSlotService.GetTimeSlotsByParishId(userId, 1)
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
                AlertMessage = alertMessageText
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
    }
}
