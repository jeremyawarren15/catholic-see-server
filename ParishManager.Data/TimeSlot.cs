using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParishManager.Data
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
    }
}
