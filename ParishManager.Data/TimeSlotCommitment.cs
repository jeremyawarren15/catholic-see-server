using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParishManager.Data
{
    public class TimeSlotCommitment
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }

        [Required]
        public virtual TimeSlot TimeSlot { get; set; }
        [Required]
        public virtual User User { get; set; }
    }
}
