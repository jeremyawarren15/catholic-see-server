﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ParishManager.Core.Entities
{
    public class SubstitutionRequest
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string UserId { get; set; }
        public string SubstitutionUserId { get; set; }
        [Required]
        public int TimeSlotCommitmentId { get; set; }
        [Required]
        public DateTime DateOfSubstitution { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [ForeignKey("SubstitutionUserId")]
        public virtual User Substitute { get; set; }
        public virtual TimeSlotCommitment TimeSlotCommitment { get; set; }
    }
}
