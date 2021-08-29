using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Services.Contracts
{
    public interface IUserService
    {
        bool IsAdminForParish(string userId, int parishId);
        bool AddUserToParish(string userId, int parishId);
        List<int> GetParishIdsForAdmin(string userId);
    }
}
