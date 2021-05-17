using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParishManager.Constants;
using ParishManager.Data.Entities;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.TimeSlot.Controllers
{
    [Area(AreaName.TimeSlot)]
    public class ClaimController : Controller
    {
        private readonly ITimeSlotCommitmentService _commitmentService;
        private readonly UserManager<User> _userManager;

        public ClaimController(
            ITimeSlotCommitmentService commitmentService,
            UserManager<User> userManager)
        {
            _commitmentService = commitmentService;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Claim(int id)
        {
            var userId = _userManager.GetUserId(User);

            _commitmentService.Claim(userId, id);

            return RedirectToAction("Index", "Home", new { area=AreaName.TimeSlot, alertMessageText = "Hour successfully claimed!" });
        }

        public IActionResult Unclaim(int id)
        {
            var userId = _userManager.GetUserId(User);

            _commitmentService.Unclaim(userId, id);

            return RedirectToAction("Index", "Home", new { area=AreaName.TimeSlot, alertMessageText = "Hour successfully unclaimed!" });
        }

    }
}
