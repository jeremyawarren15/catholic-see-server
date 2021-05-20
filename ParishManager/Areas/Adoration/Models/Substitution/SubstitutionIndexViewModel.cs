using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Areas.Adoration.Models.Substitution
{
    public class SubstitutionIndexViewModel
    {
        public IEnumerable<UnclaimedSubstitutionListItemViewModel> UnclaimedSubstitutions { get; set; }
        public string AlertMessage { get; set; }
        public int ParishId { get; set; }
        public bool IsAdmin { get; set; }
    }
}
