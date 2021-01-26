using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Core.Entities
{
    public class User : IdentityUser
    {
        public virtual ICollection<UserParishAssociation> UserParishAssociations { get; set; }
        public virtual ICollection<TimeSlotCommitment> TimeSlotCommitments { get; set; }
    }
}
