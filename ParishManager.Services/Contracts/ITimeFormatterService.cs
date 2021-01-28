using System;
using System.Collections.Generic;
using System.Text;

namespace ParishManager.Services.Contracts
{
    public interface ITimeFormatterService
    {
        string ConvertTimeToString(int hour);
    }
}
