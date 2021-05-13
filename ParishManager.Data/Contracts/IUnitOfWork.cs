using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Contracts
{
    public interface IUnitOfWork : IDisposable
    {
        IParishRepository Parishes { get; }
        ITimeSlotCommitmentRepository TimeSlotCommitments { get; }
        ITimeSlotRepository TimeSlots { get; }
        IUserParishAssociationRepository UserParishAssociations { get; }
        IUserRepository Users { get; }
        ISubstitutionRequestRepository SubstitutionRequests { get; }

        int Complete();
    }
}
