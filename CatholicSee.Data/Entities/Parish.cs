using Microsoft.AspNetCore.Identity;
using CatholicSee.Data.SharedFields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatholicSee.Data.Entities
{
    public class Parish : Base
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string ParishName { get; set; }

        public virtual ICollection<UserParishAssociation> UserParishAssociations { get; set; }
        public virtual ICollection<TimeSlot> TimeSlots { get; set; }
    }
}
