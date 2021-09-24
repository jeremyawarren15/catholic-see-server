using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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
using Microsoft.AspNetCore.Mvc;
using CatholicSee.Data.Auth;
using Microsoft.AspNetCore.Authorization;

namespace CatholicSee.Api.Controllers
{
    [ApiController]
    [Authorize]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserService _userService;
        private readonly IParishService _parishService;
        private readonly IAuthService _authService;

        private const string REFRESH_TOKEN_COOKIE_NAME = "refreshToken";

        public AuthController(
            UserManager<User> userManager,
            IUserService userService,
            IParishService parishService,
            IAuthService authService)
        {
            _userManager = userManager;
            _userService = userService;
            _parishService = parishService;
            _authService = authService;
        }

        [Route("/token")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Token(AuthenticateRequest model)
        {
            var response = _authService.Authenticate(model, GetIpAddress());
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [Route("/refresh-token")]
        [HttpPost]
        [AllowAnonymous]
        public IActionResult RefreshToken()
        {
            var refreshToken = Request.Cookies[REFRESH_TOKEN_COOKIE_NAME];
            var response = _authService.RefreshToken(refreshToken, GetIpAddress());
            SetTokenCookie(response.RefreshToken);
            return Ok(response);
        }

        [Route("/revoke-token")]
        [HttpPost]
        public IActionResult RevokeToken(RevokeTokenRequest model)
        {
            var token = model.Token ?? Request.Cookies[REFRESH_TOKEN_COOKIE_NAME];

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest("Token is required");
            }

            _authService.RevokeToken(token, GetIpAddress());

            return Ok("Token revoked");
        }

        [Route("/register")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
            };

            var registerRequest = new RegisterRequest
            {
                User = user,
                Password = model.Password
            };

            var result = _authService.Register(registerRequest);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }

                return BadRequest(result.Errors);
            }

            user = await _userManager.FindByEmailAsync(result.CreatedUser.Email);

            return Ok();

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

        private void SetTokenCookie(string token)
        {
            // append cookie with refresh token to the http response
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append(REFRESH_TOKEN_COOKIE_NAME, token, cookieOptions);
        }

        private string GetIpAddress()
        {
            // get source ip address for the current request
            if (Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                return Request.Headers["X-Forwarded-For"];
            }

            return HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
    }
}
