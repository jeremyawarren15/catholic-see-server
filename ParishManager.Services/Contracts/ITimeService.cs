using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface ITimeService
    {
        string ConvertTimeToString(int hour);
        IEnumerable<string> GetAllHours();
    }
}
