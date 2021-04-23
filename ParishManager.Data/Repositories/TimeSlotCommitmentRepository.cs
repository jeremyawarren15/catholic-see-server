using Microsoft.EntityFrameworkCore;
using ParishManager.Core.Entities;
using ParishManager.Data.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Data.Repositories
{
    public class TimeSlotCommitmentRepository : Repository<TimeSlotCommitment, int>, ITimeSlotCommitmentRepository
    {
        private readonly ApplicationDbContext _context;

        public TimeSlotCommitmentRepository(ApplicationDbContext context)
            : base(context)
        {
            _context = context;
        }
    }
}
