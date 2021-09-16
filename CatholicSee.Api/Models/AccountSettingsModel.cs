using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatholicSee.Api.Models
{
    public class AccountSettingsModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool ShouldReceiveNewHoursEmails { get; set; }
        public bool ShouldReceiveSubRequestEmails { get; set; }
    }
}
