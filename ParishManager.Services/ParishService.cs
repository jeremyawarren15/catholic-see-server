using ParishManager.Data;
using ParishManager.Models.Parish;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services
{
    public class ParishService : IParishService
    {
        private readonly ApplicationDbContext _context;

        public ParishService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Parish CreateParish(ParishCreate createModel)
        {
            using var ctx = _context;

            var parish = new Parish()
            {
                ParishName = createModel.ParishName
            };

            ctx.Parishes.Add(parish);

            ctx.SaveChanges();

            return ctx.Parishes.Find(parish);
        }

        public bool DeleteParish(int parishId)
        {
            using var ctx = _context;

            var parishToDelete = ctx.Parishes
                .SingleOrDefault(x => x.Id == parishId);

            if (parishToDelete == null)
            {
                return false;
            }

            ctx.Parishes.Remove(parishToDelete);

            return ctx.SaveChanges() != 0;
        }

        public ICollection<Parish> GetAllParishes()
        {
            using var ctx = _context;

            return ctx.Parishes
                .ToList();
        }

        public Parish GetParish(int parishId)
        {
            using var ctx = _context;

            return ctx.Parishes
                .SingleOrDefault(x => x.Id == parishId);
        }

        public Parish UpdateParish(ParishUpdate updateModel)
        {
            throw new NotImplementedException();
        }
    }
}
