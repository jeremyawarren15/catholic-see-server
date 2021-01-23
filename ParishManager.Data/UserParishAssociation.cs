using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParishManager.Data
{
    public class UserParishAssociation
    {
        public int ParishId { get; set; }
        public string UserId { get; set; }
        public bool IsRegisteredParishioner { get; set; }

        public virtual Parish Parish { get; set; }
        public virtual User User { get; set; }
    }
}
