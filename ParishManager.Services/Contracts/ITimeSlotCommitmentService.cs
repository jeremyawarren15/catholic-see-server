﻿using ParishManager.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface ITimeSlotCommitmentService
    {
        bool Claim(string user, int timeSlot);
        bool Unclaim(string user, int timeSlot);
    }
}
