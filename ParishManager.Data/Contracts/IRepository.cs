using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace ParishManager.Data.Contracts
{
    public interface IRepository<T, K> where T : class
    {
        T Get(K id);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        T Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }

    // This can be used for repositories that have
    // more than one primary key
    public interface IRepository<T, K, W> where T : class
    {
        T Get(K firstKey, W secondKey);
        IEnumerable<T> GetAll();
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

        T Add(T entity);
        void AddRange(IEnumerable<T> entities);

        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
