using ParishManager.Data.SharedFields;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParishManager.Data.Entities
{
    public class TimeSlotCommitment : Base
    {
        [Key]
        public int Id { get; set; }
        public bool Active { get; set; }
        public int TimeSlotId { get; set; }
        public string UserId { get; set; }

        [Required]
        public virtual TimeSlot TimeSlot { get; set; }
        [Required]
        public virtual User User { get; set; }
    }
}
