using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services
{
    public class ParishService : IParishService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ParishService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Parish Create(Parish entity)
        {
            var createdEntity = _unitOfWork.Parishes.Add(entity);

            _unitOfWork.Complete();

            return createdEntity;
        }

        public void Delete(Parish entity)
        {
            _unitOfWork.Parishes.Remove(entity);

            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var parishToDelete = _unitOfWork.Parishes.Get(id);

            _unitOfWork.Parishes.Remove(parishToDelete);

            _unitOfWork.Complete();
        }

        public Parish Get(int id)
        {
            return _unitOfWork.Parishes.Get(id);
        }

        public IEnumerable<Parish> GetAll()
        {
            return _unitOfWork.Parishes.GetAll();
        }

        public Parish Update(Parish entity)
        {
            var parishToUpdate = _unitOfWork.Parishes.Get(entity.Id);

            if (parishToUpdate == null)
            {
                return Create(entity);
            }

            parishToUpdate.ParishName = entity.ParishName;

            _unitOfWork.Complete();

            return parishToUpdate;
        }
    }
}
