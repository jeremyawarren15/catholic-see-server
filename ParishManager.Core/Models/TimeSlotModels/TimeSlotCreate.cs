using ParishManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Core.Models.TimeSlotModels
{
    public class TimeSlotCreate
    {
        public Parish Parish { get; set; }
        public DayOfWeek Day { get; set; }
        public int Hour { get; set; }
        public string Location { get; set; }
    }
}
