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

        public IActionResult Index()
        {
            var userId = _userManager.GetUserId(User);

            var model = _timeSlotService.GetTimeSlotsByParishId(userId, 1)
                .Select(x => new TimeSlotListItemViewModel()
                {
                    TimeSlotId = x.TimeSlotId,
                    Day = x.Day,
                    Hour = x.Hour,
                    Location = x.Location,
                    IsClaimedByUser = x.IsClaimedByUser
                });

            return View(model);
        }

        public IActionResult Claim(int id)
        {
            var userId = _userManager.GetUserId(User);

            _commitmentService.Claim(userId, id);

            TempData["alert"] = "Hour successfully claimed!";

            return RedirectToAction("Index");
        }

        public IActionResult Unclaim(int id)
        {
            var userId = _userManager.GetUserId(User);

            _commitmentService.Unclaim(userId, id);

            TempData["alert"] = "Hour successfully unclaimed!";

            return RedirectToAction("Index");
        }
    }
}
