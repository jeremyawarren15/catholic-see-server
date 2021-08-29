using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using CatholicSee.Api.Models;
using CatholicSee.Data.Entities;
using CatholicSee.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatholicSee.Api.Controllers
{
    [Route("api/availableHours")]
    [ApiController]
    [Authorize]
    public class AvailableHoursController : ControllerBase
    {
        public readonly ITimeSlotService _timeSlotService;
        public readonly UserManager<User> _userManager;

        public AvailableHoursController(ITimeSlotService timeSlotService, UserManager<User> userManager)
        {
            _timeSlotService = timeSlotService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<TimeSlotModel>> GetAsync()
        {
            var hours = _timeSlotService.GetAll();
            var user = await _userManager.GetUserAsync(User);

            return hours.Select(x => new TimeSlotModel
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

        [HttpGet("{id}")]
        public string Get(int parishId)
        {
            return "value";
        }

        // POST api/<AvailableHoursController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<AvailableHoursController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AvailableHoursController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
