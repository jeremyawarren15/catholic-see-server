using CatholicSee.Data.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Data.Models.TimeSlotModels
{
    public class TimeSlotCreate
    {
        public int ParishId { get; set; }
        public DayOfWeek Day { get; set; }
        public int Hour { get; set; }
        public string Location { get; set; }
        public bool Enabled { get; set; }
        public int MinimumNumberOfAdorers { get; set; }
    }
}
