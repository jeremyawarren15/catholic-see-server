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

        public SubRequestController(UserManager<User> userManager, ISubstitutionRequestService substitutionRequestService)
        {
            _userManager = userManager;
            _substitutionRequestService = substitutionRequestService;
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

            var savedCorrectly = _substitutionRequestService.Create(dto) != null;

            if (savedCorrectly)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpPost("cancel/{id}")]
        public async Task<ActionResult> Cancel(int id)
        {
            // need to check if the user is supposed to be able to do this
            var success = _substitutionRequestService.Delete(id);
            if (success)
            {
                return Ok();
            }

            return BadRequest();
        }
    }
}
