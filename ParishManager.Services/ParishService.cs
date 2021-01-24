using ParishManager.Data;
using ParishManager.Models.ParishModels;
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
            var parish = new Parish()
            {
                ParishName = createModel.ParishName
            };

            var createdParish = _context.Parishes.Add(parish);

            _context.SaveChanges();

            return createdParish.Entity;
        }

        public bool DeleteParish(int parishId)
        {
            var parishToDelete = _context.Parishes
                .SingleOrDefault(x => x.Id == parishId);

            _context.Parishes.Remove(parishToDelete);

            return _context.SaveChanges() != 0;
        }

        public ICollection<Parish> GetAllParishes()
        {
            return _context.Parishes
                .ToList();
        }

        public Parish GetParish(int parishId)
        {
            return _context.Parishes
                .SingleOrDefault(x => x.Id == parishId);
        }

        public Parish UpdateParish(ParishUpdate updateModel)
        {
            var parishToUpdate = _context.Parishes
                .SingleOrDefault(x => x.Id == updateModel.Id);

            parishToUpdate.ParishName = updateModel.ParishName;

            _context.SaveChanges();

            return parishToUpdate;
        }
    }
}
