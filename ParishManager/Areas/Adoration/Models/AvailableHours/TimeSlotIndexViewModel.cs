﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.Adoration.Models.AvailableHours
{
    public class TimeSlotIndexViewModel
    {
        public IEnumerable<TimeSlotListItemViewModel> TimeSlots { get; set; }
        public string AlertMessage { get; set; }
        public int ParishId { get; set; }
        public bool IsAdmin { get; set; }
    }
}