using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatholicSee.Services.Contracts
{
    public interface IService<T, K> where T : class
    {
        T Get(K id);
        T Get(T entity);
        IEnumerable<T> GetAll();
        bool Delete(K id);
        bool Delete(T entity);
    }
}
