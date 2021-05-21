﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.Adoration.Models.Substitution
{
    public class UnclaimedSubstitutionListItemViewModel
    {
        public int SubstitutionId { get; set; }
        public int TimeSlotId { get; set; }
        public DayOfWeek Day { get; set; }
        public string Hour { get; set; }
        public DateTime DateOfSubstitution { get; set; }
        public string Location { get; set; }
    }
}