using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ParishManager.Models.SubstitutionRequest
{
    public class SubstitutionRequestCreateViewModel
    {
        [Required]
        public int TimeSlotId { get; set; }
        [Required]
        public DateTime DateOfSubstitution { get; set; }

        public string HourString { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public string ParishName { get; set; }
        public List<SelectListItem> UpcomingDates { get; set; }
    }
}
