using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatholicSee.Api.Models
{
    public class SubRequestListItem
    {
        public int SubRequestId { get; set; }
        public DateTime DateOfSubstitution { get; set; }
        public int TimeSlotHour { get; set; }
        public string Location { get; set; }
    }
}
