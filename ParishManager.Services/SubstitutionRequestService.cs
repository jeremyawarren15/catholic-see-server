using ParishManager.Data;
using ParishManager.Data.Entities;
using ParishManager.Data.Models.SubstitutionRequest;
using ParishManager.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Services
{
    public class SubstitutionRequestService : ServiceBase<SubstitutionRequest, int>, ISubstitutionRequestService
    {
        private readonly ApplicationDbContext _context;

        public SubstitutionRequestService(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public SubstitutionRequest Create(SubstitutionRequestCreate createModel)
        {
            var timeSlotCommitmentId = _context.TimeSlotCommitments
                .SingleOrDefault(x => x.TimeSlotId == createModel.TimeSlotId && x.UserId == createModel.UserId)
                .Id;

            var entity = new SubstitutionRequest()
            {
                TimeSlotCommitmentId = timeSlotCommitmentId,
                DateOfSubstitution = createModel.DateOfSubstitution,
                CreatedDate = DateTime.Now,
                UserId = createModel.UserId
            };

            entity = _context.SubstitutionRequests
                .Add(entity)
                .Entity;

            _context.SaveChanges();

            return entity;
        }

        public IEnumerable<SubstitutionRequest> GetUnclaimedSubstitutionRequests(int parishId)
        {
            return _context.SubstitutionRequests
                .Where(x => x.TimeSlotCommitment.TimeSlot.ParishId == parishId);
        }
    }
}
