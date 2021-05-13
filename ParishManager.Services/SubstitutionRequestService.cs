using ParishManager.Core.Entities;
using ParishManager.Core.Models.SubstitutionRequest;
using ParishManager.Data.Contracts;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services
{
    public class SubstitutionRequestService : ISubstitutionRequestService
    {
        private readonly IUnitOfWork _unitOfWork;

        public SubstitutionRequestService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool CreateSubstitutionRequest(SubstitutionRequestCreate createModel)
        {
            var timeSlot = _unitOfWork.TimeSlots.Get(createModel.TimeSlotId);
            var timeSlotCommitment = timeSlot.TimeSlotCommitments
                .SingleOrDefault(x => x.User.Id == createModel.UserId);

            if (timeSlotCommitment == null)
            {
                return false;
            }

            var entity = new SubstitutionRequest()
            {
                UserId = createModel.UserId,
                TimeSlotCommitmentId = timeSlotCommitment.Id,
                CreatedDate = DateTime.UtcNow,
                DateOfSubstitution = createModel.DateOfSubstitution,
            };

            _unitOfWork.SubstitutionRequests
                .Add(entity);

            return _unitOfWork.Complete() == 1;
        }

        public bool Delete(int id)
        {
            var request = _unitOfWork.SubstitutionRequests.Get(id);

            _unitOfWork.SubstitutionRequests.Remove(request);

            return _unitOfWork.Complete() == 1;
        }

        public SubstitutionRequest Get(int id)
        {
            return _unitOfWork.SubstitutionRequests.Get(id);
        }

        public IEnumerable<SubstitutionRequest> GetAll()
        {
            return _unitOfWork.SubstitutionRequests.GetAll();
        }
    }
}
