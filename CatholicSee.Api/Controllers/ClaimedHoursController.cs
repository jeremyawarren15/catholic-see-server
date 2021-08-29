using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CatholicSee.Api.Models;
using CatholicSee.Data.Entities;
using CatholicSee.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatholicSee.Api.Controllers
{
    [Route("api/claimedHours")]
    [ApiController]
    public class ClaimedHoursController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITimeSlotCommitmentService _timeSlotCommitmentService;
        private readonly ISubstitutionRequestService _substitutionRequestService;

        public ClaimedHoursController(
            UserManager<User> userManager,
            ITimeSlotCommitmentService timeSlotCommitementService,
            ISubstitutionRequestService substitutionRequestService)
        {
            _userManager = userManager;
            _timeSlotCommitmentService = timeSlotCommitementService;
            _substitutionRequestService = substitutionRequestService;
        }

        [HttpGet]
        public async Task<IEnumerable<TimeSlotModel>> Get()
        {
            var user = await _userManager.GetUserAsync(User);

            return _timeSlotCommitmentService.GetCommitments(user.Id)
                .Select(x => x.TimeSlot)
                .Select(x => new TimeSlotModel
                {
                    TimeSlotId = x.Id,
                    Day = x.Day,
                    Hour = x.Hour,
                    AdorerCount = x.TimeSlotCommitments.Count,
                    IsClaimedByUser = x.TimeSlotCommitments.Any(x => x.User == user),
                    Location = x.Location,
                    MinimumAdorers = x.MinimumNumberOfAdorers,
                    ParishId = x.ParishId
                });
        }
    }
}
