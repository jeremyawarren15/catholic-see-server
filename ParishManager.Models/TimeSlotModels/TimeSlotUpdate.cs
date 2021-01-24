using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Models.TimeSlotModels
{
    public class TimeSlotUpdate
    {
        public int Id { get; set; }
        public string Location { get; set; }
        
        // hour and day are left out because
        // it seems silly to move those details
        // but I expect there to be more here
        // at some point
    }
}
