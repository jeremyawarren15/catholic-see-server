using CatholicSee.Data.SharedFields;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CatholicSee.Data.Entities
{
    public class TimeSlot : Base
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(300)]
        public string Location { get; set; }
        [Required]
        public int Hour { get; set; }
        [Required]
        public DayOfWeek Day { get; set; }
        public int MinimumNumberOfAdorers { get; set; }
        public bool Enabled { get; set; }
        public int ParishId { get; set; }

        [Required]
        public virtual Parish Parish { get; set; }
        public virtual ICollection<TimeSlotCommitment> TimeSlotCommitments { get; set; }
    }
}
