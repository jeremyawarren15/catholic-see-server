using ParishManager.Data.Contracts;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool IsAdminForParish(string userId, int parishId)
        {
            var association = _unitOfWork.UserParishAssociations.Get(parishId, userId);

            if (association == null || !association.IsAdmin)
            {
                return false;
            }

            return true;
        }
    }
}
