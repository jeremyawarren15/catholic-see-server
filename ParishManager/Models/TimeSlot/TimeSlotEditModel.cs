using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Models.TimeSlot
{
    public class TimeSlotEditModel
    {
        public int TimeSlotId { get; set; }
        public DayOfWeek Day { get; set; }
        [DisplayName("Hour")]
        public string HourText { get; set; }
        public string Location { get; set; }
        public bool Enabled { get; set; }
        [DisplayName("Minimum Required Adorers")]
        public int MinimumRequiredAdorers  { get; set; }
        public IEnumerable<TimeSlotEditUserList> CommittedAdorers { get; set; }
    }
}
