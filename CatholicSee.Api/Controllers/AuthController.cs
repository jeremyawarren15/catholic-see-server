using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.IdentityModel.Tokens;
using CatholicSee.Api.Models;
using CatholicSee.Data.Entities;
using CatholicSee.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CatholicSee.Api.Controllers
{
    [ApiController]
    public class AuthController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IParishService _parishService;

        public AuthController(UserManager<User> userManager, IUserService userService, IParishService parishService)
        {
            _userManager = userManager;
            _userService = userService;
            _parishService = parishService;
        }

        [Route("/token")]
        [HttpPost]
        public async Task<IActionResult> Create(TokenModel model)
        {
            if (await IsValidUsernameAndPassword(model.UserName, model.Password))
            {
                var token = await GenerateToken(model.UserName);

                Response.Cookies.Append(
                    "jwt-token",
                    token.Access_Token,
                    new CookieOptions
                    {
                        HttpOnly = true
                    });

                return new ObjectResult(token);
            }
            else
            {
                return BadRequest();
            }
        }

        [Route("/register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            throw new NotImplementedException();
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest();
            //}

            //var user = new User
            //{
            //    UserName = model.Email,
            //    Email = model.Email,
            //    FirstName = model.FirstName,
            //    LastName = model.LastName,
            //};

            //var result = await _userManager.CreateAsync(user, model.Password);
            //if (!result.Succeeded)
            //{
            //    foreach (var error in result.Errors)
            //    {
            //        ModelState.AddModelError(string.Empty, error.Description);
            //    }

            //    return BadRequest();
            //}

            //user = await _userManager.FindByEmailAsync(user.Email);
            //// We want to default this user to be a parishioner
            //// of St. John the Evangelist until more parishes
            //// are supported. St. Johns should be the first parish.
            //_userService.AddUserToParish(user.Id, 1);

            //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            //code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //var callbackUrl = Url.Page(
            //    "/Account/ConfirmEmail",
            //    pageHandler: null,
            //    values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
            //    protocol: Request.Scheme);

            //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
            //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
        }

        private async Task<bool> IsValidUsernameAndPassword(string username, string password)
        {
            var user = await _userManager.FindByEmailAsync(username);
            return await _userManager.CheckPasswordAsync(user, password);
        }

        private async Task<dynamic> GenerateToken(string userName)
        {
            var user = await _userManager.FindByEmailAsync(userName);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, userName),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(JwtRegisteredClaimNames.Nbf, new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds().ToString()),
                new Claim(JwtRegisteredClaimNames.Exp, new DateTimeOffset(DateTime.Now.AddDays(1)).ToUnixTimeSeconds().ToString())
            };

            var token = new JwtSecurityToken(
                new JwtHeader(
                    new SigningCredentials(
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ThisIsASecretKey")),
                        SecurityAlgorithms.HmacSha256)),
                new JwtPayload(claims));

            var output = new
            {
                Access_Token = new JwtSecurityTokenHandler().WriteToken(token),
                UserName = userName,
                ParishAdminAccessIds = _userService.GetParishIdsForAdmin(user.Id),
                Name = user.FirstName + " " + user.LastName
            };

            return output;
        }
    }
}
