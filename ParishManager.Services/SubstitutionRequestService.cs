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

        public bool Create(SubstitutionRequestCreate createModel)
        {
            throw new NotImplementedException();
        }
    }
}
