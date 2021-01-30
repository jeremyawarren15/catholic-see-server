using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Models.TimeSlot
{
    public class TimeSlotCreateViewModel
    {
        public int ParishId { get; set; }
        public DayOfWeek Day { get; set; }
        public int Hour { get; set; }
        public IEnumerable<SelectListItem> HoursList { get; set; }
        public string Location { get; set; }
        public bool Enabled { get; set; }
    }
}
