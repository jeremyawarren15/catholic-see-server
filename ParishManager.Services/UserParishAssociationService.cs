using ParishManager.Data;
using ParishManager.Models.UserParishAssociationModels;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services
{
    public class UserParishAssociationService : IUserParishAssociationService
    {
        private readonly ApplicationDbContext _context;

        public UserParishAssociationService(ApplicationDbContext context)
        {
            _context = context;
        }

        public UserParishAssociation CreateUserParishAssociation(UserParishAssociationCreate createModel)
        {
            var association = new UserParishAssociation()
            {
                UserId = createModel.UserId,
                ParishId = createModel.ParishId,
                IsRegisteredParishioner = createModel.IsRegisteredParishioner
            };

            var createdAssociation = _context.UserParishAssociations
                .Add(association);

            _context.SaveChanges();

            return createdAssociation.Entity;
        }

        public bool DeleteUSerParishAssociation(string userId, int parishId)
        {
            var association = GetUserParishAssociation(userId, parishId);

            _context.Remove(association);

            return _context.SaveChanges() != 0;
        }

        public ICollection<UserParishAssociation> GetAllUserParishAssociations()
        {
            return _context.UserParishAssociations
                .ToList();
        }

        public UserParishAssociation GetUserParishAssociation(string userId, int parishId)
        {
            return _context.UserParishAssociations
                .SingleOrDefault(x => x.UserId == userId && x.ParishId == parishId);
        }

        public ICollection<UserParishAssociation> GetUserParishAssociationsForParish(int parishId)
        {
            return _context.UserParishAssociations
                .Where(x => x.ParishId == parishId)
                .ToList();
        }

        public ICollection<UserParishAssociation> GetUserParishAssociationsForUser(string userId)
        {
            return _context.UserParishAssociations
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public bool RegisterUserToParish(string userId, int parishId)
        {
            return SetIsRegisteredParishioner(true, userId, parishId);
        }

        public bool UnregisterUserFromParish(string userId, int parishId)
        {
            return SetIsRegisteredParishioner(true, userId, parishId);
        }

        private bool SetIsRegisteredParishioner(bool isRegistered, string userId, int parishId)
        {
            var association = GetUserParishAssociation(userId, parishId);

            if (association == null)
            {
                // This means that there isn't
                // an association present for
                // the given user and parish
                return false;
            }

            association.IsRegisteredParishioner = true;

            return _context.SaveChanges() != 0;
        }
    }

}
