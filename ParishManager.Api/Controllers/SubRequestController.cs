using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ParishManager.Api.Models;
using ParishManager.Data.Entities;
using ParishManager.Data.Models.SubstitutionRequest;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ParishManager.Api.Controllers
{
    [Route("api/subRequest")]
    [ApiController]
    public class SubRequestController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ISubstitutionRequestService _substitutionRequestService;
        private readonly ITimeSlotCommitmentService _timeSlotCommitmentService;

        public SubRequestController(
            UserManager<User> userManager,
            ISubstitutionRequestService substitutionRequestService,
            ITimeSlotCommitmentService timeSlotCommitmentService)
        {
            _userManager = userManager;
            _substitutionRequestService = substitutionRequestService;
            _timeSlotCommitmentService = timeSlotCommitmentService;
        }

        [HttpPost("create")]
        public async Task<ActionResult> Create(SubRequestCreateModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            var dto = new SubstitutionRequestCreate
            {
                UserId = user.Id,
                DateOfSubstitution = model.DateOfSubstitution,
                TimeSlotId = model.TimeSlotId
            };

            var succeeded = _substitutionRequestService.Create(dto) != null;

            return GetConditionalResult(succeeded);
        }

        [HttpPost("cancel/{id}")]
        public async Task<ActionResult> Cancel(int id)
        {
            // need to check if the user is supposed to be able to do this
            var succeeded = _substitutionRequestService.Delete(id);

            return GetConditionalResult(succeeded);
        }

        [HttpPost("pickUp/{id}")]
        public async Task<ActionResult> PickUp(int id)
        {
            var user = await _userManager.GetUserAsync(User);

            var succeeded = _substitutionRequestService.PickUpHour(id, user.Id);

            return GetConditionalResult(succeeded);
        }

        [HttpGet("/api/subRequests/personal")]
        public async Task<IEnumerable<HourSubRequestListItem>> GetPersonal()
        {
            var user = await _userManager.GetUserAsync(User);

            return GetSubRequestModel(
                _substitutionRequestService.GetPersonalSubRequestsForUser(user.Id));
        }

        [HttpGet("/api/subRequests/claimed")]
        public async Task<IEnumerable<HourSubRequestListItem>> GetClaimed()
        {
            var user = await _userManager.GetUserAsync(User);

            return GetSubRequestModel(
                _substitutionRequestService.GetClaimedSubRequestsForUser(user.Id));
        }

        [HttpGet("/api/subRequests/")]
        public async Task<IEnumerable<SubRequestListItem>> Get()
        {
            var user = await _userManager.GetUserAsync(User);

            return _substitutionRequestService.GetUnclaimedSubstitutionRequests(1)
                .Where(x => x.UserId != user.Id)
                .Select(x => new SubRequestListItem
                {
                    SubRequestId = x.Id,
                    DateOfSubstitution = x.DateOfSubstitution,
                    Location = x.TimeSlotCommitment.TimeSlot.Location,
                    TimeSlotHour = x.TimeSlotCommitment.TimeSlot.Hour
                });
        }

        private ActionResult GetConditionalResult(bool succeeded)
        {
            return succeeded ? Ok() : BadRequest();
        }

        private IEnumerable<HourSubRequestListItem> GetSubRequestModel(IEnumerable<SubstitutionRequest> subRequests)
        {
            return subRequests
                .Select(x => new HourSubRequestListItem
                {
                    SubRequestId = x.Id,
                    HasBeenPickedUp = !string.IsNullOrEmpty(x.SubstitutionUserId),
                    DateOfSubstitution = x.DateOfSubstitution.ToShortDateString(),
                });
        }
    }
}
