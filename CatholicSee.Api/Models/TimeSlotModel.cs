using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatholicSee.Api.Models
{
    public class TimeSlotModel
    {
        public int TimeSlotId { get; set; }
        public DayOfWeek Day { get; set; }
        public int Hour { get; set; }
        public int ParishId { get; set; }
        public bool IsClaimedByUser { get; set; }
        public string Location { get; set; }
        public int MinimumAdorers { get; set; }
        public int AdorerCount { get; set; }
    }
}
