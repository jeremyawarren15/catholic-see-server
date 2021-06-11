using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Entities
{
    public class User : IdentityUser
    {
        public virtual ICollection<UserParishAssociation> UserParishAssociations { get; set; }
        public virtual ICollection<TimeSlotCommitment> TimeSlotCommitments { get; set; }
        public virtual ICollection<TimeSlotCommitment> ModifiedTimeSlots { get; set; }
        public virtual ICollection<UserParishAssociation> ModifiedUserParishAssociations { get; set; }
    }
}
