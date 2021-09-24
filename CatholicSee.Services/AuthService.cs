using CatholicSee.Data;
using CatholicSee.Data.Auth;
using CatholicSee.Data.Entities;
using CatholicSee.Data.Exceptions;
using CatholicSee.Data.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BCryptNet = BCrypt.Net.BCrypt;

namespace CatholicSee.Services.Contracts
{
    public class AuthService : IAuthService
    {
        private ApplicationDbContext _context;
        private IJwtService _jwtService;
        private readonly AppSettings _appSettings;

        public AuthService(
            ApplicationDbContext context,
            IJwtService jwtService,
            IOptions<AppSettings> appSettings)
        {
            _context = context;
            _jwtService = jwtService;
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress)
        {
            var user = _context.Users.SingleOrDefault(x => x.Email == model.Email);

            // validate
            if (user == null || !BCryptNet.Verify(model.Password, user.PasswordHash))
                throw new AppException("Username or password is incorrect");

            // authentication successful so generate jwt and refresh tokens
            var jwtToken = _jwtService.GenerateJwtToken(user);
            var refreshToken = _jwtService.GenerateRefreshToken(ipAddress);
            user.RefreshTokens.Add(refreshToken);

            // remove old refresh tokens from user
            removeOldRefreshTokens(user);

            // save changes to db
            _context.Update(user);
            _context.SaveChanges();

            return new AuthenticateResponse(user, jwtToken, refreshToken.Token);
        }

        public RegisterResponse Register(RegisterRequest model)
        {
            var errors = new List<RegisterError>();
            var user = _context.Users.Where(x => x.Email == model.User.Email);

            if (user.Any())
            {
                errors.Add(new RegisterError
                {
                    Code = "email",
                    Description = "A user already exists with that email address"
                });
                return GetFailedResponse(errors);
            }

            var newUser = model.User;

            newUser.PasswordHash = BCryptNet.HashPassword(model.Password);

            var createdUser = _context.Users.Add(newUser);

            _context.SaveChanges();

            var association = new UserParishAssociation()
            {
                ParishId = 1,
                UserId = createdUser.Entity.Id,
                IsRegisteredParishioner = true,
                CreatedById = createdUser.Entity.Id
            };

            _context.UserParishAssociations.Add(association);

            if (_context.SaveChanges() > 0)
            {
                return GetSuccessfulResponse(createdUser.Entity);
            }

            errors.Add(new RegisterError
            {
                Code = string.Empty,
                Description = "Something happened while creating your user."
            });
            return GetFailedResponse(errors);
        }

        private RegisterResponse GetFailedResponse(IEnumerable<RegisterError> errors)
        {
            return new RegisterResponse
            {
                Succeeded = false,
                Errors = errors
            };
        }

        private RegisterResponse GetSuccessfulResponse(User createdUser)
        {
            return new RegisterResponse
            {
                Succeeded = true,
                Errors = new List<RegisterError>(),
                CreatedUser = createdUser
            };
        }

        public AuthenticateResponse RefreshToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (refreshToken.IsRevoked)
            {
                // revoke all descendant tokens in case this token has been compromised
                revokeDescendantRefreshTokens(refreshToken, user, ipAddress, $"Attempted reuse of revoked ancestor token: {token}");
                _context.Update(user);
                _context.SaveChanges();
            }

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            // replace old refresh token with a new one (rotate token)
            var newRefreshToken = rotateRefreshToken(refreshToken, ipAddress);
            user.RefreshTokens.Add(newRefreshToken);

            // remove old refresh tokens from user
            removeOldRefreshTokens(user);

            // save changes to db
            _context.Update(user);
            _context.SaveChanges();

            // generate new jwt
            var jwtToken = _jwtService.GenerateJwtToken(user);

            return new AuthenticateResponse(user, jwtToken, newRefreshToken.Token);
        }

        public void RevokeToken(string token, string ipAddress)
        {
            var user = getUserByRefreshToken(token);
            var refreshToken = user.RefreshTokens.Single(x => x.Token == token);

            if (!refreshToken.IsActive)
                throw new AppException("Invalid token");

            // revoke token and save
            revokeRefreshToken(refreshToken, ipAddress, "Revoked without replacement");
            _context.Update(user);
            _context.SaveChanges();
        }

        // helper methods

        private User getUserByRefreshToken(string token)
        {
            var user = _context.Users.SingleOrDefault(u => u.RefreshTokens.Any(t => t.Token == token));

            if (user == null)
                throw new AppException("Invalid token");

            return user;
        }

        private RefreshToken rotateRefreshToken(RefreshToken refreshToken, string ipAddress)
        {
            var newRefreshToken = _jwtService.GenerateRefreshToken(ipAddress);
            revokeRefreshToken(refreshToken, ipAddress, "Replaced by new token", newRefreshToken.Token);
            return newRefreshToken;
        }

        private void removeOldRefreshTokens(User user)
        {
            // remove old inactive refresh tokens from user based on TTL in app settings
            user.RefreshTokens.RemoveAll(x => 
                !x.IsActive && 
                x.Created.AddDays(_appSettings.RefreshTokenTTL) <= DateTime.UtcNow);
        }

        private void revokeDescendantRefreshTokens(RefreshToken refreshToken, User user, string ipAddress, string reason)
        {
            // recursively traverse the refresh token chain and ensure all descendants are revoked
            if(!string.IsNullOrEmpty(refreshToken.ReplacedByToken))
            {
                var childToken = user.RefreshTokens.SingleOrDefault(x => x.Token == refreshToken.ReplacedByToken);
                if (childToken.IsActive)
                    revokeRefreshToken(childToken, ipAddress, reason);
                else
                    revokeDescendantRefreshTokens(childToken, user, ipAddress, reason);
            }
        }

        private void revokeRefreshToken(RefreshToken token, string ipAddress, string reason = null, string replacedByToken = null)
        {
            token.Revoked = DateTime.UtcNow;
            token.RevokedByIp = ipAddress;
            token.ReasonRevoked = reason;
            token.ReplacedByToken = replacedByToken;
        }

    }
}
