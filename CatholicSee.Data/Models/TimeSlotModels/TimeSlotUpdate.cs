﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CatholicSee.Data.Models.TimeSlotModels
{
    public class TimeSlotUpdate
    {
        public int Id { get; set; }
        public string Location { get; set; }
        public int MinimumNumberOfAdorers { get; set; }
        public bool Enabled { get; set; }
    }
}
