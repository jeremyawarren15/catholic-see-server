using ParishManager.Data;
using ParishManager.Models.UserParishAssociationModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface IUserParishAssociationService
    {
        UserParishAssociation CreateUserParishAssociation(UserParishAssociationCreate createModel);
        ICollection<UserParishAssociation> GetAllUserParishAssociations();
        ICollection<UserParishAssociation> GetUserParishAssociationsForParish(int parishId);
        ICollection<UserParishAssociation> GetUserParishAssociationsForUser(string userId);
        UserParishAssociation GetUserParishAssociation(string userId, int parishId);
        bool RegisterUserToParish(string userId, int parishId);
        bool UnregisterUserFromParish(string userId, int parishId);
        bool DeleteUSerParishAssociation(string userId, int parishId);
    }
}
