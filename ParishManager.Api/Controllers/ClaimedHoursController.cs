using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParishManager.Api.Models;
using ParishManager.Data.Entities;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParishManager.Api.Controllers
{
    [Route("api/claimedHours")]
    [ApiController]
    public class ClaimedHoursController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ITimeSlotCommitmentService _timeSlotCommitmentService;

        public ClaimedHoursController(UserManager<User> userManager, ITimeSlotCommitmentService timeSlotCommitementService)
        {
            _userManager = userManager;
            _timeSlotCommitmentService = timeSlotCommitementService;
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
                    SubRequests =
                        x.TimeSlotCommitments
                            .Where(x => x.User == user)
                            .SelectMany(x => x.SubstitutionRequests.Select(x => new SubRequestListItem
                            {
                                SubRequestId = x.Id,
                                HasBeenPickedUp = string.IsNullOrEmpty(x.SubstitutionUserId),
                                DateOfSubstitution = x.DateOfSubstitution.ToShortDateString(),
                            })),
                    ParishId = x.ParishId
                });
        }
    }
}
