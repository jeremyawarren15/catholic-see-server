using CatholicSee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Services.Contracts
{
    public interface IParishService : IService<Parish, int>
    {
        public Parish Create(Parish entity);
        public Parish Update(Parish entity);
    }
}
