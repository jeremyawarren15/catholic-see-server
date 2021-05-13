using ParishManager.Data.Entities;
using ParishManager.Data.Models.SubstitutionRequest;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface ISubstitutionRequestService : IService<SubstitutionRequest, int>
    {
        bool Create(SubstitutionRequestCreate createModel);
    }
}
