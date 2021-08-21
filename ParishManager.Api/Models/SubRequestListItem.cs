using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Api.Models
{
    public class SubRequestListItem
    {
        public int SubRequestId { get; set; }
        public bool HasBeenPickedUp { get; set; }
        public string DateOfSubstitution { get; set; }
    }
}
