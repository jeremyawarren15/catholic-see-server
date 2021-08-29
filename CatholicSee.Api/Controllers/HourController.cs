using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CatholicSee.Data.Entities;
using CatholicSee.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatholicSee.Api.Controllers
{
    [Route("api/hour")]
    [ApiController]
    [Authorize]
    public class HourController : ControllerBase
    {
        private readonly ITimeSlotCommitmentService _timeSlotCommitmentService;
        private readonly UserManager<User> _userManager;

        public HourController(ITimeSlotCommitmentService timeSlotCommitmentService, UserManager<User> userManager)
        {
            _timeSlotCommitmentService = timeSlotCommitmentService;
            _userManager = userManager;
        }

        [HttpPost("claim/{id}")]
        public async Task<ActionResult> Claim(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var succeeded = await _timeSlotCommitmentService.ClaimAsync(user.Id, id);

            return Ok(new { succeeded });
        }

        [HttpPost("unclaim/{id}")]
        public async Task<ActionResult> Unclaim(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var succeeded = await _timeSlotCommitmentService.UnclaimAsync(user.Id, id);

            return Ok(new { succeeded });
        }
    }
}
