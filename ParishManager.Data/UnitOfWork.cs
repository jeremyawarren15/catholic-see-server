using ParishManager.Data.Contracts;
using ParishManager.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IParishRepository Parishes { get; private set; }
        public ITimeSlotCommitmentRepository TimeSlotCommitments { get; private set; }
        public ITimeSlotRepository TimeSlots { get; private set; }
        public IUserParishAssociationRepository UserParishAssociations { get; private set; }
        public IUserRepository Users { get; private set; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;

            Parishes = new ParishRepository(_context);
            TimeSlotCommitments = new TimeSlotCommitmentRepository(_context);
            TimeSlots = new TimeSlotRepository(_context);
            UserParishAssociations = new UserParishAssociationRepository(_context);
            Users = new UserRepository(_context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
