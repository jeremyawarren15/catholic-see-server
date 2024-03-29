﻿using CatholicSee.Data;
using CatholicSee.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatholicSee.Services
{
    public abstract class ServiceBase<T, K> : IService<T, K> where T : class
    {
        private readonly ApplicationDbContext _context;

        public ServiceBase(ApplicationDbContext context)
        {
            _context = context;
        }

        public bool Delete(K id)
        {
            var entityToDelete = Get(id);

            return Delete(entityToDelete);
        }

        public bool Delete(T entity)
        {
            _context.Remove<T>(entity);

            return _context.SaveChanges() != 0;
        }

        public T Get(K id)
        {
            return _context.Set<T>().Find(id);
        }

        public T Get(T entity)
        {
            return _context.Entry(entity).Entity;
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>();
        }
    }
}
