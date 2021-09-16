using CatholicSee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Services.Contracts
{
    public interface IJwtService
    {
        public string GenerateJwtToken(User user);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
