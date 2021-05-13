using ParishManager.Data;
using ParishManager.Data.Entities;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services
{
    public class ParishService : ServiceBase<Parish, int>, IParishService
    {
        private readonly ApplicationDbContext _context;

        public ParishService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public Parish Create(Parish entity)
        {
            var returnedEntity = _context.Parishes
                .Add(entity)
                .Entity;

            _context.SaveChanges();

            return returnedEntity;
        }

        public Parish Update(Parish entity)
        {
            var parishToUpdate = Get(entity);

            if (parishToUpdate == null)
            {
                return Create(entity);
            }

            parishToUpdate.ParishName = entity.ParishName;

            _context.SaveChanges();

            return parishToUpdate;
        }
    }
}
