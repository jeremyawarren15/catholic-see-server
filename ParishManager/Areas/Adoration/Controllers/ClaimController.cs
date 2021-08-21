using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParishManager.Constants;
using ParishManager.Data.Entities;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.Adoration.Controllers
{
    [Area(AreaName.Adoration)]
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

        public async Task<IActionResult> Claim(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            await _commitmentService.ClaimAsync(user.Id, id);

            return RedirectToAction("Index", "AvailableHours", new { area=AreaName.Adoration, alertMessageText = "Hour successfully claimed!" });
        }

        public async Task<IActionResult> Unclaim(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            await _commitmentService.UnclaimAsync(user.Id, id);

            return RedirectToAction("Index", "Home", new { area=AreaName.Adoration, alertMessageText = "Hour successfully unclaimed!" });
        }

    }
}
