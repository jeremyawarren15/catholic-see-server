using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParishManager.Core.Entities
{
    public class TimeSlot
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(300)]
        public string Location { get; set; }
        [Required]
        public int Hour { get; set; }
        [Required]
        public DayOfWeek Day { get; set; }

        [Required]
        public virtual Parish Parish { get; set; }
        public virtual ICollection<TimeSlotCommitment> TimeSlotCommitments { get; set; }
    }
}
