using CatholicSee.Data.Auth;
using CatholicSee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Services.Contracts
{
    public interface IAuthService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddres);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        RegisterResponse Register(RegisterRequest model);
        void RevokeToken(string token, string ipAddress);
    }
}
