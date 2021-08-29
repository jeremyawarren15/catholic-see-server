using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatholicSee.Api.Models
{
    public class SubRequestCreateModel
    {
        public int TimeSlotId { get; set; }
        public DateTime DateOfSubstitution { get; set; }
    }
}
