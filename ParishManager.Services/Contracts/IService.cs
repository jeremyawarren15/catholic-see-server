﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface IService<T, K> where T : class
    {
        T Create(T entity);

        T Get(K id);
        IEnumerable<T> GetAll();

        T Update(T entity);

        void Delete(T entity);
        void Delete(K id);
    }
}