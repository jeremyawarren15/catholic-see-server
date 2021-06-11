using ParishManager.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ParishManager.Data.SharedFields
{
    public class Base
    {
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public DateTime? LastModifiedDate { get; set; }
        public virtual User CreatedBy { get; set; }
        public virtual string CreatedById { get; set; }
        public virtual User LastModifiedBy { get; set; }
        public virtual string LastModifiedById { get; set; }
    }
}
