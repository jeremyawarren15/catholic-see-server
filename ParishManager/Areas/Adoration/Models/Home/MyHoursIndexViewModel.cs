using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.Adoration.Models.Home
{
    public class MyHoursIndexViewModel
    {
        public IEnumerable<MyHoursListItemViewModel> MyHours { get; set; }
        public string AlertMessage { get; set; }
        public int ParishId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
