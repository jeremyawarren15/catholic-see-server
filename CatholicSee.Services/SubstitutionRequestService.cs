using CatholicSee.Data;
using CatholicSee.Data.Entities;
using CatholicSee.Data.Models.SubstitutionRequest;
using CatholicSee.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatholicSee.Services
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

        public IEnumerable<SubstitutionRequest> GetPersonalSubRequestsForUser(string userId)
        {
            return _context.SubstitutionRequests
                .Where(x => x.UserId == userId);
        }

        public IEnumerable<SubstitutionRequest> GetClaimedSubRequestsForUser(string userId)
        {
            return _context.SubstitutionRequests
                .Where(x => x.SubstitutionUserId == userId);
        }

        public IEnumerable<SubstitutionRequest> GetUnclaimedSubstitutionRequests(int parishId)
        {
            return _context.SubstitutionRequests
                .Where(x => x.TimeSlotCommitment.TimeSlot.ParishId == parishId && x.Substitute == null);
        }

        public bool PickUpHour(int substitutionRequestId, string userId)
        {
            var request = _context.SubstitutionRequests
                .SingleOrDefault(x => x.Id == substitutionRequestId);

            request.LastModifiedDate = DateTime.Now;
            request.LastModifiedById = userId;
            request.SubstitutionUserId = userId;

            return _context.SaveChanges() != 0;
        }
    }
}
