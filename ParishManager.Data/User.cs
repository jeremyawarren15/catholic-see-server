using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data
{
    public class User : IdentityUser
    {
        public virtual ICollection<UserParishAssociation> UserParishAssociations { get; set; }
    }
}
