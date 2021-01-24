using ParishManager.Data;
using ParishManager.Models.UserModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface IUserService
    {
        User CreateUser(UserCreate createModel);
        ICollection<User> GetAllUsers();
        ICollection<User> GetParishioners(int parishId);
        ICollection<User> GetNonParishioners(int parishId);
        ICollection<User> GetAssociatedUsersForParish(int parishId);
        ICollection<User> GetAllUsersForTimeSlot(int timeSlotId);
        User GetUser(string userId);
        User UpdateUser(UserUpdate updateModel);
        bool DeleteUser(int userId);
    }
}
