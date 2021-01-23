using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParishManager.Data
{
    public class Parish
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string ParishName { get; set; }

        public virtual ICollection<UserParishAssociation> UserParishAssociations { get; set; }
    }
}
