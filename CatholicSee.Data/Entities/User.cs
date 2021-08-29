using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatholicSee.Data.Entities
{
    public class User : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        public virtual ICollection<UserParishAssociation> UserParishAssociations { get; set; }
        public virtual ICollection<TimeSlotCommitment> TimeSlotCommitments { get; set; }
        public virtual ICollection<TimeSlotCommitment> ModifiedTimeSlots { get; set; }
        public virtual ICollection<UserParishAssociation> ModifiedUserParishAssociations { get; set; }
    }
}
