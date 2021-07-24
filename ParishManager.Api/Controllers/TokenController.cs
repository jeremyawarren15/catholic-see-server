using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using ParishManager.Api.Models;
using ParishManager.Data.Entities;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ParishManager.Api.Controllers
{
    [ApiController]
    public class TokenController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;

        public TokenController(UserManager<User> userManager, IUserService userService)
        {
            _userManager = userManager;
            _userService = userService;
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
