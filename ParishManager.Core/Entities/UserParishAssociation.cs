﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ParishManager.Core.Entities
{
    public class UserParishAssociation
    {
        public int ParishId { get; set; }
        public string UserId { get; set; }
        [DefaultValue(false)]
        public bool IsRegisteredParishioner { get; set; }
        [DefaultValue(false)]
        public bool IsAdmin { get; set; }

        public virtual Parish Parish { get; set; }
        public virtual User User { get; set; }
    }
}
