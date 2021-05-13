using ParishManager.Core.Entities;
using ParishManager.Core.Models.SubstitutionRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface ISubstitutionRequestService : IService<SubstitutionRequest, int>
    {
        bool CreateSubstitutionRequest(SubstitutionRequestCreate createModel);
    }
}
