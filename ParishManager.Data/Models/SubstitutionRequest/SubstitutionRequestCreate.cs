using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Data.Models.SubstitutionRequest
{
    public class SubstitutionRequestCreate
    {
        public string UserId { get; set; }
        public int TimeSlotId { get; set; }
        public DateTime DateOfSubstitution { get; set; }
    }
}
