using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.Adoration.Models.Home
{
    public class TimeSlotCreateViewModel
    {
        public int ParishId { get; set; }
        public DayOfWeek Day { get; set; }
        public int Hour { get; set; }
        public IEnumerable<SelectListItem> HoursList { get; set; }
        public string Location { get; set; }
        public bool Enabled { get; set; }
        [DisplayName("Minimum Number of Adorers")]
        public int MinimumNumberOfAdorers { get; set; }
    }
}
