using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ParishManager.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // entities with multiple keys must be defined this way
            builder.Entity<UserParishAssociation>()
                .HasKey(t => new { t.ParishId, t.UserId });

            base.OnModelCreating(builder);
        }

        DbSet<Parish> Parishes { get; set; }
        DbSet<UserParishAssociation> UserParishAssociations { get; set; }
        DbSet<TimeSlot> TimeSlots { get; set; }
        DbSet<TimeSlotCommitment> TimeSlotCommitments { get; set; }
    }
}
