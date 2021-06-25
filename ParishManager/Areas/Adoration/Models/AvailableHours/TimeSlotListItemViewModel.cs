﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.Adoration.Models.AvailableHours
{
    public class TimeSlotListItemViewModel
    {
        public int TimeSlotId { get; set; }
        public DayOfWeek Day { get; set; }
        public string Hour { get; set; }
        public string Location { get; set; }
        public bool IsClaimedByUser { get; set; }
        public int CommittedAdorers { get; set; }
        public int MinimumAdorers { get; set; }
        public decimal AdorerCommitmentProgress { get; set; }
    }
}
