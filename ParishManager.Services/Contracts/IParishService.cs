using ParishManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface IParishService : IService<Parish, int>
    {
        public Parish Create(Parish entity);
        public Parish Update(Parish entity);
    }
}
