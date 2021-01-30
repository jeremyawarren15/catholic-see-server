using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface IService<T, K> where T : class
    {
        T Get(K id);
        IEnumerable<T> GetAll();
        bool Delete(K id);
    }
}
