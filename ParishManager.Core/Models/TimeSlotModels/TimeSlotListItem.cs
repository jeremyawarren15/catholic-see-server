using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Core.Models.TimeSlotModels
{
    public class TimeSlotListItem
    {
        public int TimeSlotId { get; set; }
        public DayOfWeek Day { get; set; }
        public string Hour { get; set; }
        public string Location { get; set; }
        public bool IsClaimedByUser { get; set; }
    }
}
