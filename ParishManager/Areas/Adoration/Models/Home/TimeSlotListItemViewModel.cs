using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.Adoration.Models.Home
{
    public class TimeSlotListItemViewModel
    {
        public int TimeSlotId { get; set; }
        public DayOfWeek Day { get; set; }
        public string Hour { get; set; }
        public string Location { get; set; }
        public bool IsClaimedByUser { get; set; }
    }
}
