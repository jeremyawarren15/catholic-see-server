using CatholicSee.Api.Models;
using CatholicSee.Data.Entities;
using CatholicSee.Services.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CatholicSee.Api.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public AccountController(
            UserManager<User> userManager,
            IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
        }

        // GET: api/account
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return BadRequest("Your user is not in the database.");
            }

            var model = new AccountSettingsModel
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                ShouldReceiveNewHoursEmails = user.ShouldReceiveNewHourEmail,
                ShouldReceiveSubRequestEmails = user.ShouldReceiveSubRequestsEmail
            };

            return Ok(model);
        }

        // GET api/account/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            throw new NotImplementedException();
        }

        // PUT api/account/
        [HttpPut]
        public async Task<ActionResult> Put([FromBody] AccountSettingsModel model)
        {
            var user = await _userManager.GetUserAsync(User);

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.ShouldReceiveNewHourEmail = model.ShouldReceiveNewHoursEmails;
            user.ShouldReceiveSubRequestsEmail = model.ShouldReceiveSubRequestEmails;

            var result = _userService.Update(user);

            if (result == null)
            {
                return StatusCode(500);
            }

            return Ok();
        }

        // PUT api/account/5
        [HttpPut("{id}")]
        public void PutForOtherUser(int id, [FromBody] string value)
        {
            throw new NotImplementedException();
        }
    }
}
