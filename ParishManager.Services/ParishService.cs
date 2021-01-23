using ParishManager.Data;
using ParishManager.Models.Parish;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services
{
    public class ParishService : IParishService
    {
        public Parish CreateParish(ParishCreate createModel)
        {
            throw new NotImplementedException();
        }

        public bool DeleteParish(int parishId)
        {
            throw new NotImplementedException();
        }

        public ICollection<Parish> GetAllParishes()
        {
            return new List<Parish>();
        }

        public Parish GetParish(int parishId)
        {
            throw new NotImplementedException();
        }

        public Parish UpdateParish(ParishUpdate updateModel)
        {
            throw new NotImplementedException();
        }
    }
}
