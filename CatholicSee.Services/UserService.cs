using CatholicSee.Data;
using CatholicSee.Data.Entities;
using CatholicSee.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatholicSee.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool AddUserToParish(string userId, int parishId)
        {
            var association = new UserParishAssociation()
            {
                UserId = userId,
                ParishId = parishId,
                CreatedById = userId,
                CreatedDate = DateTime.UtcNow,
                IsRegisteredParishioner = true
            };

            _context.UserParishAssociations
                .Add(association);

            return _context.SaveChanges() > 0;
        }

        public List<int> GetParishIdsForAdmin(string userId)
        {
            return _context.UserParishAssociations
                .Where(x => x.UserId == userId && x.IsAdmin)
                .Select(x => x.ParishId)
                .ToList();
        }

        public bool IsAdminForParish(string userId, int parishId)
        {
            var association = _context.UserParishAssociations
                .SingleOrDefault(x => x.ParishId == parishId && x.UserId == userId);

            if (association == null || !association.IsAdmin)
            {
                return false;
            }

            return true;
        }
    }
}
