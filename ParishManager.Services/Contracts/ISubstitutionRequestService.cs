using ParishManager.Data.Entities;
using ParishManager.Data.Models.SubstitutionRequest;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ParishManager.Services.Contracts
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
