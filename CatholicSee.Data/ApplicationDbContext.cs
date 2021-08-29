using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using CatholicSee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CatholicSee.Data
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

            builder.Entity<TimeSlotCommitment>()
                .HasOne(x => x.User)
                .WithMany(x => x.TimeSlotCommitments)
                .HasForeignKey("UserId");
            builder.Entity<TimeSlotCommitment>()
                .HasOne(x => x.LastModifiedBy)
                .WithMany(x => x.ModifiedTimeSlots);

            builder.Entity<UserParishAssociation>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserParishAssociations)
                .HasForeignKey("UserId");
            builder.Entity<UserParishAssociation>()
                .HasOne(x => x.LastModifiedBy)
                .WithMany(x => x.ModifiedUserParishAssociations);
                
            base.OnModelCreating(builder);
        }

        public DbSet<Parish> Parishes { get; set; }
        public DbSet<UserParishAssociation> UserParishAssociations { get; set; }
        public DbSet<TimeSlot> TimeSlots { get; set; }
        public DbSet<TimeSlotCommitment> TimeSlotCommitments { get; set; }
        public DbSet<SubstitutionRequest> SubstitutionRequests { get; set; }
    }
}
