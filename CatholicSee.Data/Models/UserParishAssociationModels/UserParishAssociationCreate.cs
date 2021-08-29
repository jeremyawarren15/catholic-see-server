using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Data.Models.UserParishAssociationModels
{
    public class UserParishAssociationCreate
    {
        public string UserId { get; set; }
        public int ParishId { get; set; }
        public bool IsRegisteredParishioner { get; set; }
    }
}
