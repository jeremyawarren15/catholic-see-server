using CatholicSee.Data.Entities;
using CatholicSee.Data.Models.SubstitutionRequest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CatholicSee.Services.Contracts
{
    public interface ISubstitutionRequestService : IService<SubstitutionRequest, int>
    {
        SubstitutionRequest Create(SubstitutionRequestCreate createModel);
        IEnumerable<SubstitutionRequest> GetUnclaimedSubstitutionRequests(int parishId);
        bool PickUpHour(int substitutionRequestId, string userId);
        IEnumerable<SubstitutionRequest> GetPersonalSubRequestsForUser(string userId);
        IEnumerable<SubstitutionRequest> GetClaimedSubRequestsForUser(string userId);
    }
}
