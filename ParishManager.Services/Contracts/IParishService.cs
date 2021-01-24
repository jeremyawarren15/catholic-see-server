﻿using ParishManager.Data;
using ParishManager.Models.ParishModels;
using System;
using System.Collections.Generic;

namespace ParishManager.Services.Contracts
{
    public interface IParishService
    {
        Parish CreateParish(ParishCreate createModel);
        Parish GetParish(int parishId);
        ICollection<Parish> GetAllParishes();
        Parish UpdateParish(ParishUpdate updateModel);
        bool DeleteParish(int parishId);
    }
}
